using Core.Data;
using System;

namespace Core.Machine
{
    public interface IBaseMachine : IDisposable
    {
        bool IsDisposed { get; }
        bool IsActive { get; }
        void Initialize();
        void Stop();
        void Resume();
        void TriggerUpdate();
        void TriggerFixedUpdate();
        void TriggerLateUpdate();
    }

    public interface IMachine<TData> : IBaseMachine, IDisposable where TData : IData
    {
        IState<TData> CurrentState { get; }
        TData Data { get; }
        void SetState<T>() where T : IState<TData>;
    }
}
