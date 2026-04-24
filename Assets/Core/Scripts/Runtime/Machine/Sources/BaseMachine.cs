using Core.Data;
using Zenject;

namespace Core.Machine
{
    public abstract class BaseMachine<TData> : IMachine<TData> where TData : IData, new()
    {
        public bool IsDisposed { get; private set; }
        public bool IsActive { get; private set; }
        public IState<TData> CurrentState { get; private set; }
        public TData Data { get; private set; } = new();

        [Inject] private readonly DiContainer _diContainer;

        public void TriggerFixedUpdate()
        {
            FixedUpdate();
        }

        public void TriggerLateUpdate()
        {
            LateUpdate();
        }

        public abstract void Initialize();

        public void TriggerUpdate()
        {
            Update();
        }

        public void SetExternalData(TData data)
        {
            Data = data;
        }

        public void SetState<T>() where T : IState<TData>
        {
            CurrentState?.TriggerExit();
            CurrentState?.Dispose();

            CurrentState = _diContainer.Instantiate<T>();
            CurrentState.Initialize(this);

            IsActive = true;

            CurrentState.TriggerEnter();
        }

        public void Stop()
        {
            IsActive = false;
            CurrentState?.TriggerStop();
        }

        public void Resume()
        {
            IsActive = true;
            CurrentState?.TriggerResume();
        }

        protected virtual void Update()
        {
            CurrentState?.TriggerUpdate();
        }

        protected virtual void FixedUpdate()
        {
            CurrentState?.TriggerFixedUpdate();
        }

        protected virtual void LateUpdate()
        {
            CurrentState?.TriggerLateUpdate();
        }

        protected virtual void OnDispose()
        {
          
        }

        public void Dispose()
        {
            if(IsDisposed)
            {
                return;
            }

            CurrentState?.TriggerExit();
            CurrentState?.Dispose();
            OnDispose();

            IsDisposed = true;
            IsActive = false;
        }

    }
}
