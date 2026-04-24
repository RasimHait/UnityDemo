using System;
using Newtonsoft.Json;

namespace Core.Data
{
    [Serializable]
    public abstract class BaseData<TBase> : IData where TBase : BaseData<TBase>, new()
    {
        public TBase CloneData()
        {
            return (TBase)Clone();
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public void Deserialize(string serialized)
        {
            JsonConvert.PopulateObject(serialized, this);
        }
    }
}
