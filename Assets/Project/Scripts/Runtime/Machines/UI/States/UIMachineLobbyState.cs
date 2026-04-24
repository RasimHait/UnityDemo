using Core.Machine;
using UnityEngine;
using Zenject;
using UniRx;
using Project.Services;
using Project.Data;

namespace Project.Machines
{
    public class UIMachineLobbyState : BaseState<UIDynamicData>
    {
        [Inject] private readonly UIService _uiService;
        [Inject] private readonly EventService _eventService;

        protected override void Enter()
        {
            Debug.Log("Project: UI Machine entered Lobby State");
            _uiService.HUD.GameLobbyScreen.Show();

            _eventService.ObserveEvent<EventData.UI.TapOnButton>()
                .Where(x => x.Tag == "BUTTON_LOBBY_PLAY")
                .Subscribe(x => OnPlayButtonClicked())
                .AddTo(LifeTime);
        }

        protected override void Exit()
        {
            _uiService.HUD.GameLobbyScreen.Hide();
        }

        private void OnPlayButtonClicked()
        {
           Machine.SetState<UIMachineActiveState>();
        }
    }
}
