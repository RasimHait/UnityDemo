using Core.Data;
using System;

namespace Project.Data
{
    public class ProgressDynamicData : BaseData<ProgressDynamicData>
    {
        public LevelProgressData Level = new();
    }

    [Serializable]
    public class LevelProgressData
    {
        public int Active;
        public int Passed;
    }
}
