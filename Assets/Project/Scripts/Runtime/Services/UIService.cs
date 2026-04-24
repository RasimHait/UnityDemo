using Core.Services;
using Project.UI;
using UnityEngine;
using Zenject;

namespace Project.Services
{
    public class UIService : BaseService
    {
        public GameHUD HUD { get; private set; }
        [Inject] private readonly ContentService _contentService;
     
        protected override void OnInitialize()
        {
            HUD = Instantiator.InstantiatePrefabForComponent<GameHUD>(_contentService.StaticData.GameHUDPrefab);
            Object.DontDestroyOnLoad(HUD);
        }
    }
}
