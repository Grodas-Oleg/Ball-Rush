using _DunkBall.Scripts.Components;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _DunkBall.Scripts.UI
{
    public class PauseWindowView : BaseView
    {
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _restartButton;
        [Inject] private GameUI _gameUI;

        private void Awake()
        {
            _resumeButton.onClick.AddListener(() => _gameUI.SwitchPauseWindow(false));
            _restartButton.onClick.AddListener(ReloadLevel.Reload);
            _hiderButton.onClick.AddListener(() => _gameUI.SwitchPauseWindow(false));
        }

        private void OnEnable() => Time.timeScale = 0f;

        protected override void OnDisable()
        {
            base.OnDisable();
            Time.timeScale = 1f;
        }
    }
}