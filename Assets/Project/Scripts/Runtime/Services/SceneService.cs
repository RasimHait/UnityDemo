using Core.Services;
using Zenject;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

namespace Project.Services
{
    public class SceneService : BaseService
    {
        [Inject] private readonly ContentService _contentService;


        public void LoadMain(LoadSceneMode mode = LoadSceneMode.Single)
        {
            if (GetActiveSceneIndex() == _contentService.StaticData.MainSceneIndex)
            {
                return;
            }
         
            var parameters = new LoadSceneParameters(mode);
            SceneManager.LoadScene(_contentService.StaticData.MainSceneIndex, parameters);

        }

        public async UniTask LoadMainAsync(LoadSceneMode mode = LoadSceneMode.Single)
        {
            if (GetActiveSceneIndex() == _contentService.StaticData.MainSceneIndex)
            {
                return;
            }

            var parameters = new LoadSceneParameters(mode);
            await SceneManager.LoadSceneAsync(_contentService.StaticData.MainSceneIndex, parameters).ToUniTask();
        }

        public void LoadInitial(LoadSceneMode mode = LoadSceneMode.Single)
        {
            if (GetActiveSceneIndex() == _contentService.StaticData.InitialSceneIndex)
            {
                return;
            }

            var parameters = new LoadSceneParameters(mode);
            SceneManager.LoadScene(_contentService.StaticData.InitialSceneIndex, parameters);

        }

        public async UniTask LoadInitialAsync(LoadSceneMode mode = LoadSceneMode.Single)
        {
            if (GetActiveSceneIndex() == _contentService.StaticData.InitialSceneIndex)
            {
                return;
            }

            var parameters = new LoadSceneParameters(mode);
            await SceneManager.LoadSceneAsync(_contentService.StaticData.InitialSceneIndex, parameters).ToUniTask();
        }

        public int GetActiveSceneIndex()
        {
            return SceneManager.GetActiveScene().buildIndex;
        }
    }
}
