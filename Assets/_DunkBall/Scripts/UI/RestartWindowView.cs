using System.Collections;
using _DunkBall.Scripts.Components;
using UnityEngine;
using UnityEngine.UI;

namespace _DunkBall.Scripts.UI
{
    public class RestartWindowView : BaseView
    {
        [SerializeField] private float _autoRespawnTime;
        [SerializeField] private Button _respawnButton;
        [SerializeField] private Image _progressBar;

        private void Awake() => _respawnButton.onClick.AddListener(ReloadLevel.Reload);

        private IEnumerator StartTimer()
        {
            var wff = new WaitForEndOfFrame();
            var elapsedTime = 0f;
            _progressBar.fillAmount = 0;

            while (elapsedTime < _autoRespawnTime)
            {
                elapsedTime += Time.unscaledDeltaTime;
                _progressBar.fillAmount = elapsedTime / _autoRespawnTime;
                yield return wff;
            }

            _progressBar.fillAmount = 0;
            ReloadLevel.Reload();
        }

        private void OnEnable()
        {
            Time.timeScale = 0f;
            StartCoroutine(StartTimer());
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            Time.timeScale = 1f;
        }
    }
}