using Core.Machine;
using Project.Data;
using Project.Machines;
using Project.Services;
using UniRx;
using UnityEngine;
using Zenject;

namespace Project
{
    public class LevelMachineVictoryState : BaseState<LevelDynamicData>
    {
        [Inject] private readonly EventService _eventService;
        [Inject] private readonly ProgressService _progressService;

        protected override void Enter()
        {
            Debug.Log("Project: Level Machine entered Victory State");

            _progressService.Data.Level.Active++;
            _progressService.Data.Level.Passed++;
            _progressService.Save();

            _eventService.ObserveEvent<EventData.UI.TapOnButton>()
                .Where(x => x.Tag == "BUTTON_VICTORY_CONTINUE")
                .Subscribe(_ => BackToLobby())
                .AddTo(LifeTime);

            _eventService.TriggerEvent(new EventData.Level.Completed(true));

        }

        private void BackToLobby()
        {
            Machine.SetState<LevelMachineLobbyState>();
        }
    }
}
