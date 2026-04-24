using UnityEngine;

namespace Core.Processors
{
    public interface IPoolProcessor
    {
        void ClearPool(string poolName);
        T Pop<T>(string poolName) where T : IPoolableObject;
        T Pop<T>(string poolName, Transform parent) where T : IPoolableObject;
        void Push<T>(string poolName, T reference) where T : IPoolableObject;
        void PushBack<T>(T obj) where T : IPoolableObject;
    }
}
