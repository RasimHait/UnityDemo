namespace Core.Processors
{
    public interface IPoolableObject
    {
        void OnAddToPool();
        void OnPopFromPool();
        void OnReturnToPool();
    }
}
