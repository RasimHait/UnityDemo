using Core.UI;
using TMPro;
using UnityEngine;

namespace Project.UI
{
    public class GameActiveScreenView : BaseUIScreen
    {
        [SerializeField] private TMP_Text _levelLabel;
        [SerializeField] private TMP_Text _scoreLabel;

        public void SetLevel(int level)
        {
            _levelLabel.text = $"LEVEL {level}";
        }

        public void SetScore(int score)
        {
            _scoreLabel.text = score.ToString();
        }
    }
}
