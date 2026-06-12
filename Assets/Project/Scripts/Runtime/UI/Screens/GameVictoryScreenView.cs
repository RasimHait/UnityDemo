using Core.UI;
using TMPro;
using UnityEngine;

namespace Project.UI
{
    public class GameVictoryScreenView : BaseUIScreen
    {
        [SerializeField] private TMP_Text _levelLabel;

        public void SetLevel(int level)
        {
            _levelLabel.text = $"LEVEL {level}";
        }
    }
}
