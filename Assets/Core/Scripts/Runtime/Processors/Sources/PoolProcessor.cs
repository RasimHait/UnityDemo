using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Core.Processors
{
    public class PoolProcessor : IPoolProcessor
    {
        private readonly Dictionary<string, Queue<IPoolableObject>> _pools = new();
        private readonly Dictionary<IPoolableObject, string> _out = new();
        private readonly Dictionary<string, Transform> _parentContainers = new();
        private IInstantiator _instantiator;
        private Transform _root;

        public void Initialize(IInstantiator instantiator, Transform root)
        {
            _instantiator = instantiator;
            _root = root;
        }

        public void Push<T>(string poolName, T reference) where T : IPoolableObject
        {
            var prefab = reference as MonoBehaviour;

            if (!_pools.ContainsKey(poolName))
            {
                _pools.Add(poolName, new());

                if (prefab)
                {
                    var container = new GameObject("POOL CONTAINER: " + poolName).transform;
                    container.SetParent(_root);
                    _parentContainers.Add(poolName, container);
                }
            }

            if (prefab)
            {
                var newObject = _instantiator.InstantiatePrefabForComponent<T>(prefab, _parentContainers[poolName]);
                newObject.OnAddToPool();
                _pools[poolName].Enqueue(newObject);
            }
            else
            {
                var newObject = _instantiator.Instantiate<T>();
                newObject.OnAddToPool();
                _pools[poolName].Enqueue(newObject);
            }
        }

        public void PushBack<T>(T obj) where T : IPoolableObject
        {
            if (_out.ContainsKey(obj))
            {
                var poolName = _out[obj];
                _out.Remove(obj);
                _pools[poolName].Enqueue(obj);

                if (obj is MonoBehaviour prefab)
                {
                    prefab.transform.SetParent(_parentContainers[poolName]);
                }

                obj.OnReturnToPool();
            }
        }

        public T Pop<T>(string poolName) where T : IPoolableObject
        {
            if (_pools.ContainsKey(poolName) && _pools[poolName].Count > 0)
            {
                var obj = (T)_pools[poolName].Dequeue();
                _out.Add(obj, poolName);
                obj.OnPopFromPool();
                return obj;
            }

            Debug.LogError($"PoolProcessor: No objects in pool with name {poolName}");
            return default;
        }

        public T Pop<T>(string poolName, Transform parent) where T : IPoolableObject
        {
            var obj = Pop<T>(poolName);

            if (obj != null && obj is MonoBehaviour prefab)
            {
                prefab.transform.SetParent(parent);
            }

            return obj;
        }

        public void ClearPool(string poolName)
        {
            if (_pools.TryGetValue(poolName, out var pool))
            {
                foreach (var obj in pool)
                {
                    if (obj is MonoBehaviour prefab)
                    {
                        UnityEngine.Object.Destroy(prefab.gameObject);
                    }
                }

                pool.Clear();
            }
        }
    }
}
