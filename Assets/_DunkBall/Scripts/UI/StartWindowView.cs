using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _DunkBall.Scripts.UI
{
    public class StartWindowView : BaseView
    {
        [SerializeField] private Button _startGameButton;
        [Inject] private GameUI _gameUI;

        private void Awake()
        {
            _startGameButton.onClick.AddListener(StatGame);
            _hiderButton.onClick.AddListener(StatGame);
        }

        private void StatGame()
        {
            Time.timeScale = 1f;
            _gameUI.HideStartWindow();
        }

        private void OnEnable() => Time.timeScale = 0f;
    }
}