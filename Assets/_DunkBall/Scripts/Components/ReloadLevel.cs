using _DunkBall.Scripts.Audio;
using _DunkBall.Scripts.Data;
using _DunkBall.Scripts.EventLayer;
using _DunkBall.Scripts.UI;
using _DunkBall.Scripts.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace _DunkBall.Scripts.Components
{
    public class ReloadLevel : Singleton<ReloadLevel>
    {
        private const string GAME_OVER = "game_over";

        [SerializeField] private TriggerComponent _trigger;
        [Inject] private GameUI _gameUI;

        protected override void OnAwake() => _trigger.AddCallbacks(other =>
        {
            _gameUI.SwitchRestartWindow(true);
            AudioHelper.PlaySoundByName(GAME_OVER);
        });

        public static void Reload()
        {
            EventBus.OnGameOver?.Invoke();
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
            EventBus.Clear();
        }

        private void OnApplicationQuit()
        {
            var data = DataSaver.GlobalData;
            data.CurrentScore = 0;
            DataSaver.GlobalData = data;
        }
    }
}