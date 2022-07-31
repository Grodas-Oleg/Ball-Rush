using _DunkBall.Scripts.Components;
using UnityEngine;
using UnityEngine.UI;

namespace _DunkBall.Scripts.UI
{
    public class PauseWindowView : BaseView
    {
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _restartButton;

        // private void Awake()
        // {
        //     _resumeButton.onClick.AddListener(() => GameUI.SwitchPauseWindow(false));
        //     _restartButton.onClick.AddListener(ReloadLevel.Reload);
        // }

        private void OnEnable() => Time.timeScale = 0f;

        protected override void OnDisable()
        {
            base.OnDisable();
            Time.timeScale = 1f;
        }
    }
}