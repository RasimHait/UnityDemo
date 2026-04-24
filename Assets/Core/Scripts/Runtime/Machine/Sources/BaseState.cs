using Core.Data;
using Core.Processors;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;

namespace Core.Machine
{
    public abstract class BaseState<TData> : IState<TData> where TData : IData
    {
        public bool IsActive { get; private set; }
        public bool IsStopped { get; private set; }
        public bool IsDisposed { get; private set; }
        public IMachine<TData> Machine { get; private set; }
        public TData Data => Machine.Data;

        protected readonly LifeTimeProcessor LifeTime = new();

        public void Initialize(IMachine<TData> machine)
        {
            Machine = machine;
        }

        public void TriggerFixedUpdate()
        {
            if(!IsActive || IsStopped)
            {
                return;
            }

            FixedUpdate();
        }

        public void TriggerLateUpdate()
        {
            if (!IsActive || IsStopped)
            {
                return;
            }

            LateUpdate();
        }

        public void TriggerUpdate()
        {
            if (!IsActive || IsStopped)
            {
                return;
            }

            Update();
        }

        public void TriggerEnter()
        {
            if(IsActive)
            {
                return;
            }

            Enter();
            IsActive = true;
        }

        public void TriggerExit()
        {
            if(!IsActive)
            {
                return;
            }

            IsActive = false;
            Exit();
        }

        public void TriggerStop()
        {
            IsStopped = true;
            Stop();
        }

        public void TriggerResume()
        {
            IsStopped = false;
            Resume();
        }

        protected virtual void Enter()
        {

        }

        protected virtual void Exit()
        {

        }

        protected virtual void Stop()
        {

        }

        protected virtual void Resume()
        {

        }

        protected virtual void Update()
        {

        }

        protected virtual void FixedUpdate()
        {

        }

        protected virtual void LateUpdate()
        {

        }

        protected virtual void OnDispose()
        {

        }

        public virtual void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            OnDispose();
            LifeTime?.Dispose();

            IsDisposed = true;
        }

    }
}
