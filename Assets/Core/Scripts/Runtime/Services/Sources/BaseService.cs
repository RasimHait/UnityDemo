using System;
using Zenject;

namespace Core.Services
{
    public abstract class BaseService : IService, IFixedTickable, ITickable, ILateTickable, IDisposable, IInitializable
    {
        public bool IsDisposed { get; private set; }
        public IInstantiator Instantiator => _container;
        [Inject] private readonly DiContainer _container;

        public void Initialize()
        {
            OnInitialize();
        }

        public void Tick()
        {
            Update();
        }

        public void FixedTick()
        {
            FixedUpdate();
        }

        public void LateTick()
        {
            LateUpdate();
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

        protected virtual void OnInitialize()
        {

        }

        protected virtual void OnDispose()
        {

        }

        public void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            OnDispose();

            IsDisposed = true;
        }

       
    }
}