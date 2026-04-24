using Core.Services;
using Cysharp.Threading.Tasks;
using Project.Machines;
using Zenject;

namespace Project.Services
{
    public class LoopService : BaseService
    {
        public LevelMachine LevelMachine { get; private set; }
        public UIMachine UIMachine { get; private set; }

        [Inject] private readonly MachineService _machineService;
        [Inject] private readonly SceneService _sceneService;
        [Inject] private readonly ContentService _contentService;
        [Inject] private readonly PoolService _poolService;

        override protected void OnInitialize()
        {
            Start();
        }

        private async void Start()
        {
            await InitScenes();
            FillGlobalPools();
            InitMachines();
        }

        private async UniTask InitScenes()
        {
            var shouldRunFromInitial = _contentService.StaticData.RunFromInitialScene;
            var currentSceneIsInitial = _sceneService.GetActiveSceneIndex() == _contentService.StaticData.InitialSceneIndex;

            if (shouldRunFromInitial && !currentSceneIsInitial)
            {
                await _sceneService.LoadInitialAsync();
            }

            await _sceneService.LoadMainAsync();
        }

        private void InitMachines()
        {
            LevelMachine = _machineService.CreateMachine<LevelMachine>();
            UIMachine = _machineService.CreateMachine<UIMachine>();
        }

        private void FillGlobalPools()
        {
            for (int i = 0; i < 10; i++)
            {
                _poolService.Push("MergeVFX", _contentService.StaticData.MergeVFXPrefab);
            }
        }
    }
}