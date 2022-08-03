using _DunkBall.Scripts.EventLayer;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _DunkBall.Scripts.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private BaseView _startWindow;
        [SerializeField] private BaseView _pauseWindow;
        [SerializeField] private BaseView _restartWindow;
        [SerializeField] private ButtonView _pauseButton;
        [SerializeField] private Image _globalHider;
        private void Start() => _pauseWindow.Hide();

        private void Awake()
        {
            FadeGlobal(false, false, 0.5f);

            _pauseButton.Init(() => SwitchPauseWindow(true));

            if (!EventBus.isFirstLaunch.Value)
            {
                _startWindow.Show(true);
                EventBus.isFirstLaunch.Publish(true);
            }
        }

        public void HideStartWindow() => _startWindow.Hide();

        public void SwitchRestartWindow(bool flag)
        {
            if (flag)
                _restartWindow.Show();
            else
                _restartWindow.Hide();
        }

        public void SwitchPauseWindow(bool flag)
        {
            if (flag)
                _pauseWindow.Show();
            else
                _pauseWindow.Hide();
        }

        private void FadeGlobal(bool @in, bool force, float delay = 0f, float fadeTime = 0.75f)
        {
            if (force)
            {
                _globalHider.raycastTarget = @in;
                var tempColor = _globalHider.color;
                tempColor.a = @in ? 1f : 0f;
                _globalHider.color = tempColor;
                return;
            }

            if (@in)
            {
                _globalHider.raycastTarget = true;
                _globalHider
                    .DOFade(1f, fadeTime)
                    .SetDelay(delay)
                    .SetUpdate(true);
            }
            else
            {
                _globalHider
                    .DOFade(0f, fadeTime)
                    .SetDelay(delay)
                    .SetUpdate(true)
                    .OnComplete(() => _globalHider.raycastTarget = false);
            }
        }
    }
}