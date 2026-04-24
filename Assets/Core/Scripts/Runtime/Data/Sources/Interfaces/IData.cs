using System;

namespace Core.Data
{
    public interface IData : ICloneable
    {
        string Serialize();
        void Deserialize(string serialized);
    }
}
