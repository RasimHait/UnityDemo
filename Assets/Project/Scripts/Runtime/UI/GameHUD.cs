using UnityEngine;

namespace Project.UI
{
    public class GameHUD : MonoBehaviour
    {
        [field: SerializeField] public GameLoadingScreenView GameLoadingScreen { get; private set; }
        [field: SerializeField] public GameLobbyScreenView GameLobbyScreen { get; private set; }
    }
}
