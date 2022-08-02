using System;
using _DunkBall.Scripts.Data;
using _DunkBall.Scripts.EventLayer;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _DunkBall.Scripts.UI
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _starsText;
        [SerializeField] private TextMeshProUGUI _scoresRecordText;

        private void Start()
        {
            _starsText.SetText(DataSaver.GlobalData.TotalStars.ToString());
            _scoreText.SetText(DataSaver.GlobalData.CurrentScore.ToString());
            _scoresRecordText.SetText(DataSaver.GlobalData.MaxReachedScore.ToString());

            DataSaver.OnGlobalDataChanged +=
                globalData => _scoreText.SetText(DataSaver.GlobalData.CurrentScore.ToString());
            DataSaver.OnGlobalDataChanged +=
                globalData => _starsText.SetText(DataSaver.GlobalData.TotalStars.ToString());
            DataSaver.OnGlobalDataChanged +=
                globalData => _scoresRecordText.SetText(DataSaver.GlobalData.MaxReachedScore.ToString());


            EventBus.OnGameOver += OnGameOver;
        }

        private void OnGameOver()
        {
            var data = DataSaver.GlobalData;
            data.CurrentScore = 0;
            DataSaver.GlobalData = data;
        }

        private void OnDestroy() => EventBus.OnGameOver -= OnGameOver;
    }
}