using Core.Machine;
using UnityEngine;
using Zenject;
using UniRx;
using Project.Data;
using Project.Services;


namespace Project.Machines
{
    public class LevelMachineLobbyState : BaseState<LevelDynamicData>
    {
        [Inject] private readonly LevelService _levelService;
        [Inject] private readonly EventService _eventService;
        [Inject] private readonly PoolService _poolService;
        [Inject] private readonly ContentService _contentService;
     
        protected override void Enter()
        {
            Debug.Log("Project: Level Machine entered Lobby State");
            _levelService.LoadActive();

            PrepareLevel();
            FillCubePool();

            _eventService.ObserveEvent<EventData.UI.TapOnButton>()
               .Where(x => x.Tag == "BUTTON_LOBBY_PLAY")
               .Subscribe(x => OnPlayButtonClicked())
               .AddTo(LifeTime);
        }

        private void PrepareLevel()
        {
            var settings = _levelService.CurrentLevelData;
            Data.FieldObject = Object.Instantiate(settings.FieldViewPrefab);
        }

        private void FillCubePool()
        {
            var settings = _levelService.CurrentLevelData;

            for (int i = 0; i < 100; i++)
            {
                _poolService.Push("Cubes", settings.CubeViewPrefab);
            }
        }

        private void OnPlayButtonClicked()
        {
            Machine.SetState<LevelMachineActiveState>();
        }
    }
}
