using UnityEngine;

namespace Core.Processors
{
    public interface IContentProcessor
    {
        T Load<T>(string path) where T : Object;
    }
}
