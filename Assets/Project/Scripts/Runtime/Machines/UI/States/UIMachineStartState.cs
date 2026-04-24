using Core.Machine;
using Cysharp.Threading.Tasks;
using Project.Data;
using Project.Services;
using UnityEngine;
using Zenject;

namespace Project.Machines
{
    public class UIMachineStartState : BaseState<UIDynamicData>
    {
        [Inject] private readonly UIService _uiService;

        protected override void Enter()
        {
            Debug.Log("Project: UI Machine entered Start State");
            _uiService.HUD.GameLoadingScreen.Show();
            LifeTime.RunBindedNoWait(Load);
        }

        protected override void Exit()
        {
            _uiService.HUD.GameLoadingScreen.Hide();
        }

        // Some fake loading with random pauses to make it look more natural
        private async UniTask Load()
        {
            var time = 0f;
            var duration = 2f;
            var nextPauseAt = Random.Range(0.2f, 0.8f);

            while (time / duration < 1f)
            {
                var progress = time / duration;

                if (progress >= nextPauseAt)
                {
                    float pauseDuration = Random.Range(0.2f, 0.8f);
                    float pauseTime = 0f;

                    while (pauseTime < pauseDuration)
                    {
                        _uiService.HUD.GameLoadingScreen.SetProgress(progress);
                        pauseTime += Time.deltaTime;
                        await UniTask.Yield(LifeTime.Token);
                    }

                    nextPauseAt += Random.Range(0.1f, 0.3f);
                }

                _uiService.HUD.GameLoadingScreen.SetProgress(progress);

                time += Time.deltaTime;
                await UniTask.Yield(LifeTime.Token);
            }

            await UniTask.Delay(1000, true, PlayerLoopTiming.Update, LifeTime.Token);

            Machine.SetState<UIMachineLobbyState>();
        }
    }
}
