using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UniRx;

namespace Core.Processors
{
    public class LifeTimeProcessor : CompositeDisposable
    {
        private readonly CancellationTokenSource _mainCTS = new();
        public CancellationToken Token => _mainCTS.Token;

        public LifeTimeProcessor()
        {
            Add(_mainCTS);
        }

        public CancellationTokenSource GetNewTokenSource()
        {
            if (IsDisposed) throw new ObjectDisposedException(GetType().Name);

            var newCTS = new CancellationTokenSource();
            var linkedCTS = CancellationTokenSource.CreateLinkedTokenSource(_mainCTS.Token, newCTS.Token);

            Add(linkedCTS);
            return linkedCTS;
        }

        public async UniTask RunBinded(Func<UniTask> task)
        {
            if (IsDisposed) throw new ObjectDisposedException(GetType().Name);
            await task().AttachExternalCancellation(Token).SuppressCancellationThrow();
        }

        public void RunBindedNoWait(Func<UniTask> task)
        {
            RunBinded(task).Forget(UnityEngine.Debug.LogException);
        }

        public async UniTask<CancellationTokenSource> RunUnBinded(Func<CancellationToken, UniTask> taskFactory)
        {
            if (IsDisposed) throw new ObjectDisposedException(GetType().Name);
            var source = GetNewTokenSource();

            await taskFactory(source.Token).SuppressCancellationThrow();
            return source;
        }

        public CancellationTokenSource RunUnBindedNoWait(Func<CancellationToken, UniTask> taskFactory)
        {
            var source = GetNewTokenSource();
            taskFactory(source.Token).SuppressCancellationThrow().Forget(UnityEngine.Debug.LogException);
            return source;
        }

    }
}
