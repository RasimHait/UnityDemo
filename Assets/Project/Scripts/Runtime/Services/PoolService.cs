using Core.Processors;
using Core.Services;
using UnityEngine;

namespace Project.Services
{
    public class PoolService : BaseService, IPoolProcessor
    {
        private Transform _root;
        private readonly PoolProcessor _processor = new();

        protected override void OnInitialize()
        {
            _root = new GameObject("[Pools]").transform;
            Object.DontDestroyOnLoad(_root);
            _processor.Initialize(Instantiator, _root);
        }

        public T Pop<T>(string poolName) where T : IPoolableObject
        {
            return _processor.Pop<T>(poolName);
        }

        public T Pop<T>(string poolName, Transform parent) where T : IPoolableObject
        {
            return _processor.Pop<T>(poolName, parent);
        }

        public void Push<T>(string poolName, T reference) where T : IPoolableObject
        {
            _processor.Push(poolName, reference);
        }

        public void PushBack<T>(T obj) where T : IPoolableObject
        {
            _processor.PushBack(obj);
        }

        public void ClearPool(string poolName)
        {
            _processor.ClearPool(poolName);
        }
    }
}
