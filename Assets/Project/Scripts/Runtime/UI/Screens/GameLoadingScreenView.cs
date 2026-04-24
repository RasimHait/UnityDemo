using Core.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    public class GameLoadingScreenView : BaseUIScreen
    {
        [SerializeField] private Scrollbar _progressBar;

        public void SetProgress(float progress)
        {
            _progressBar.size = progress;
        }
    }
}
