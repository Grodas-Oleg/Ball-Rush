using _DunkBall.Scripts.Data;
using _DunkBall.Scripts.EventLayer;
using TMPro;
using UnityEngine;

namespace _DunkBall.Scripts.UI
{
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _starsText;

        private void Start()
        {
            EventBus.OnTotalScoreChange += () => _scoreText.SetText(DataSaver.GlobalData.TotalScore.ToString());
            EventBus.OnStarsChange += () => _starsText.SetText(DataSaver.GlobalData.TotalStars.ToString());
        }
    }
}