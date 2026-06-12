using Core.Machine;
using Project.Data;
using Project.Services;
using UniRx;
using UnityEngine;
using Zenject;

namespace Project.Machines
{
    public class UIMachineActiveState : BaseState<UIDynamicData>
    {
        [Inject] private readonly UIService _uiService;
        [Inject] private readonly EventService _eventService;
        [Inject] private readonly ProgressService _progressService;

        protected override void Enter()
        {
            Debug.Log("Project: UI Machine entered Active(Game) State");
            _uiService.HUD.GameActiveScreen.Show();
            _uiService.HUD.GameActiveScreen.SetLevel(_progressService.Data.Level.Passed + 1);
            _uiService.HUD.GameActiveScreen.SetScore(0);

            _eventService.ObserveEvent<EventData.Level.ScoreUpdated>()
                .Subscribe(OnScoreUpdated)
                .AddTo(LifeTime);

            _eventService.ObserveEvent<EventData.Level.Completed>()
                .Subscribe(OnLevelCompleted)
                .AddTo(LifeTime);
        }

        protected override void Exit()
        {
            _uiService.HUD.GameActiveScreen.Hide();
        }

        private void OnScoreUpdated(EventData.Level.ScoreUpdated data)
        {
            _uiService.HUD.GameActiveScreen.SetScore(data.Score);
        }

        private void OnLevelCompleted(EventData.Level.Completed data)
        {
            if (data.IsVictory)
            {
                Machine.SetState<UIMachineVictoryState>();
            }
            else
            {
                Machine.SetState<UIMachineLoseState>();
            }
        }
    }
}
