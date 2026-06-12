using UnityEngine;

namespace Project.UI
{
    public class GameHUD : MonoBehaviour
    {
        [field: SerializeField] public GameLoadingScreenView GameLoadingScreen { get; private set; }
        [field: SerializeField] public GameLobbyScreenView GameLobbyScreen { get; private set; }
        [field: SerializeField] public GameActiveScreenView GameActiveScreen { get; private set; }
        [field: SerializeField] public GameVictoryScreenView GameVictoryScreen { get; private set; }
    }
}
