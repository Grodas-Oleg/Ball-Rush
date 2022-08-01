using UnityEngine;
using UnityEngine.UI;

namespace _DunkBall.Scripts.UI
{
    public class StartWindowView : BaseView
    {
        [SerializeField] private Button _startGameButton;

        private void Awake()
        {
            _startGameButton.onClick.AddListener(StatGame);
            _hiderButton.onClick.AddListener(StatGame);
        }

        private void StatGame()
        {
            Time.timeScale = 1f;
            GameUI.HideStartWindow();
        }

        private void OnEnable() => Time.timeScale = 0f;
    }
}