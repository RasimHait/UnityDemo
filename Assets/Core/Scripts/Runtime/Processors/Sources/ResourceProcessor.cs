using UnityEngine;

namespace Core.Processors
{
    public class ResourceProcessor : IContentProcessor
    {
        public T Load<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }
    }
}
