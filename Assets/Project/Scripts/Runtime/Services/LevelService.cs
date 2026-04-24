using Core.Services;
using Project.Data;
using UnityEngine;
using Zenject;

namespace Project.Services
{
    public class LevelService : BaseService
    {
        public LevelStaticData CurrentLevelData { get; private set; }

        [Inject] private readonly ContentService _contentService;
        [Inject] private readonly ProgressService _progressService;

        public void LoadActive()
        {
            var levelID = ValidateLevelID(_progressService.Data.Level.Active);
            CurrentLevelData = _contentService.StaticData.Levels[levelID];
            Debug.Log($"Project: Level loaded with ID: {levelID}");
        }

        public void MarkActiveAsComplete()
        {
            _progressService.Data.Level.Active = ValidateLevelID(_progressService.Data.Level.Active + 1);
            _progressService.Data.Level.Passed++;
            _progressService.Save();
        }

        private int ValidateLevelID(int levelID)
        {
            if (levelID >= _contentService.StaticData.Levels.Count || levelID < 0)
            {
                _progressService.Data.Level.Active = _contentService.StaticData.LoopFromLevelIndex;
                _progressService.Save();
                return 0;
            }

            return levelID;
        }
    }
}
