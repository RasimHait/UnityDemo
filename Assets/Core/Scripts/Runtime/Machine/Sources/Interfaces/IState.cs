using Core.Data;
using System;

namespace Core.Machine
{
    public interface IBaseState : IDisposable
    {
        bool IsActive { get; }
        bool IsStopped { get; }
        void TriggerEnter();
        void TriggerExit();
        void TriggerStop();
        void TriggerResume();
        void TriggerUpdate();
        void TriggerFixedUpdate();
        void TriggerLateUpdate();
    }

    public interface IState<TData> : IBaseState where TData : IData
    {
        IMachine<TData> Machine { get; }
        TData Data { get; }
        void Initialize(IMachine<TData> machine);
    }
}
