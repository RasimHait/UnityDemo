using Core.Services;
using Project.Data;
using UnityEngine;

namespace Project.Services
{
    public class ProgressService : BaseService
    {
        private const string SaveKey = "Project.Progress.SaveKey";
        public readonly ProgressDynamicData Data = new();

        protected override void OnInitialize()
        {
            Load();
        }

        private void Load()
        {
            if (PlayerPrefs.HasKey(SaveKey))
            {
                var json = PlayerPrefs.GetString(SaveKey);
                Data.Deserialize(json);
            }
            else
            {
                Save();
            }
        }

        public void Save()
        {
            PlayerPrefs.SetString(SaveKey, Data.Serialize());
            PlayerPrefs.Save();
        }
    }
}
