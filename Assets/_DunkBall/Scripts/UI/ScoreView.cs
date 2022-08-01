using _DunkBall.Scripts.Data;
using _DunkBall.Scripts.EventLayer;
using TMPro;
using UnityEngine;

namespace _DunkBall.Scripts.UI
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _starsText;

        private void Start()
        {
            _starsText.SetText(DataSaver.GlobalData.TotalStars.ToString());
            _scoreText.SetText(DataSaver.GlobalData.CurrentScore.ToString());

            DataSaver.OnGlobalDataChanged +=
                globalData => _scoreText.SetText(DataSaver.GlobalData.CurrentScore.ToString());
            DataSaver.OnGlobalDataChanged +=
                globalData => _starsText.SetText(DataSaver.GlobalData.TotalStars.ToString());

            EventBus.OnGameOver += OnGameOver;
        }

        private void OnGameOver()
        {
            var data = DataSaver.GlobalData;
            if (data.CurrentScore > data.MaxReachedScore) data.MaxReachedScore = data.CurrentScore;
            data.CurrentScore = 0;
            DataSaver.GlobalData = data;
        }
    }
}