using Core.Machine;
using Project.Data;
using Project.View;
using Project.Services;
using UnityEngine;
using Zenject;
using UniRx;
using Cysharp.Threading.Tasks;

namespace Project.Machines
{
    public class LevelMachineActiveState : BaseState<LevelDynamicData>
    {
        [Inject] private readonly InputService _inputService;
        [Inject] private readonly PoolService _poolService;
        [Inject] private readonly EventService _eventService;
        private CubeView _currentCube;

        protected override void Enter()
        {
            Debug.Log("Project: Level Machine entered Active(Game) State");
            Subscribe();
            LoadCube();
        }

        private void Subscribe()
        {
            _inputService.ActiveDragDelta
                .Subscribe(x => UpdateLaunchOriginPosition(x.x))
                .AddTo(LifeTime);

            _inputService.ActiveDragEnd
                .Where(_ => _currentCube != null)
                .Subscribe(x => LifeTime.RunBindedNoWait(LaunchCube))
                .AddTo(LifeTime);


            _eventService.ObserveEvent<EventData.Cube.Contact>()
                .Subscribe(OnCubeContact)
                .AddTo(LifeTime);
        }

        private void UpdateLaunchOriginPosition(float delta)
        {
            Data.FieldObject.MoveLaunchOrigin(delta);
        }

        private void LoadCube()
        {
            _currentCube = _poolService.Pop<CubeView>("Cubes");
            Data.FieldObject.PlaceCube(_currentCube);
        }

        private async UniTask LaunchCube()
        {
            _currentCube.Launch(Vector3.forward, 100, Data.FieldObject.transform);
            _currentCube = null;

            await UniTask.Delay(1000, cancellationToken: LifeTime.Token, cancelImmediately: true);

            LoadCube();
        }

        private async void OnCubeContact(EventData.Cube.Contact data)
        {
            if (data.Target.Value != data.Initiator.Value)
            {
                return;
            }

            _poolService.PushBack(data.Initiator);

            LifeTime.RunBindedNoWait(() => ShowMergeVFX(data.ContantPoint));

            await data.Target.Upgrade();
        }

        private async UniTask ShowMergeVFX(Vector3 point)
        {
            var vfx = _poolService.Pop<ParticlesView>("MergeVFX");
            vfx.transform.position = point;

            await UniTask.Delay(3000, cancellationToken: LifeTime.Token, cancelImmediately: true);

            _poolService.PushBack(vfx);
        }
    }
}
