using Core.Machine;
using Core.Services;
using System;
using System.Collections.Generic;
using Zenject;

namespace Project.Services
{
    public class MachineService : BaseService, IMachineFactory
    {
        private readonly Dictionary<Type, HashSet<IBaseMachine>> _machines = new();
        [Inject] private readonly DiContainer _diContainer;

        public T CreateMachine<T>() where T : IBaseMachine
        {
            var machine = _diContainer.Instantiate<T>();
            
            if(_machines.TryGetValue(typeof(T), out var machines))
            {
                machines.Add(machine);
            }
            else
            {
                _machines[typeof(T)] = new HashSet<IBaseMachine> { machine };
            }

            machine.Initialize();
            return machine;
        }

        public void DestroyMachine(IBaseMachine machine)
        {
            machine.Dispose();
        }

        public IEnumerable<IBaseMachine> GetMachines<T>() where T : IBaseMachine
        {
            if (_machines.TryGetValue(typeof(T), out var machines))
            {
                foreach (var m in machines)
                {
                    if (!m.IsDisposed)
                        yield return m;
                }
            }
        }

        public void DestroyMachines<T>() where T : IBaseMachine
        {
            if (_machines.TryGetValue(typeof(T), out var machines))
            {
                foreach (var machine in machines)
                {
                    machine.Dispose();
                }
            }
        }

        private void ClearDisposedMachines()
        {
            foreach (var machineSet in _machines.Values)
            {
                machineSet.RemoveWhere(machine => machine.IsDisposed);
            }
        }

        protected override void Update()
        {
            base.Update();

            ClearDisposedMachines();

            foreach (var machineSet in _machines.Values)
            {
                foreach (var machine in machineSet)
                {
                    machine.TriggerUpdate();
                }
            }
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            ClearDisposedMachines();

            foreach (var machineSet in _machines.Values)
            {
                foreach (var machine in machineSet)
                {
                    machine.TriggerFixedUpdate();
                }
            }
        }

        protected override void LateUpdate()
        {
            base.LateUpdate();

            ClearDisposedMachines();

            foreach (var machineSet in _machines.Values)
            {
                foreach (var machine in machineSet)
                {
                    machine.TriggerLateUpdate();
                }
            }
        }
    }
}
