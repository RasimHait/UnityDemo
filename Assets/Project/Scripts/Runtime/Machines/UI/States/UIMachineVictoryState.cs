using Core.Machine;
using Project.Data;
using Project.Services;
using UnityEngine;
using UniRx;
using Zenject;

namespace Project.Machines
{
    public class UIMachineVictoryState : BaseState<UIDynamicData>
    {
        [Inject] private readonly UIService _uiService;
        [Inject] private readonly ProgressService _progressService;
        [Inject] private readonly EventService _eventService;

        protected override void Enter()
        {
            Debug.Log("Project: UI Machine entered Victory State");
            _uiService.HUD.GameVictoryScreen.Show();
            _uiService.HUD.GameVictoryScreen.SetLevel(_progressService.Data.Level.Passed);


            _eventService.ObserveEvent<EventData.UI.TapOnButton>()
                .Where(x => x.Tag == "BUTTON_VICTORY_CONTINUE")
                .Subscribe(_ => BackToLobby())
                .AddTo(LifeTime);
        }

        protected override void Exit()
        {
            _uiService.HUD.GameVictoryScreen.Hide();
        }

        private void BackToLobby()
        {
            Machine.SetState<UIMachineLobbyState>();
        }
    }
}
