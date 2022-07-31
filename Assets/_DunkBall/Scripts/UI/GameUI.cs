using _DunkBall.Scripts.Utilities;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _DunkBall.Scripts.UI
{
    public class GameUI : Singleton<GameUI>
    {
        [SerializeField] private ButtonView _pauseButton;
        [SerializeField] private BaseView _pauseWindow;
        [SerializeField] private Image _globalHider;
        private void Start() => _pauseWindow.Hide();

        protected override void OnAwake()
        {
            base.OnAwake();
            _pauseButton.Init(() => SwitchPauseWindow(true));
        }

        public static void SwitchInteractablePauseButton(bool flag) => Instance._pauseButton.SwitchInteractable(flag);

        public static void SwitchPauseWindow(bool flag)
        {
            if (flag)
            {
                Instance._pauseWindow.Show();
            }
            else
                Instance._pauseWindow.Hide();
        }

        public static void FadeGlobal(bool @in, bool force, float delay = 0f, float fadeTime = 0.75f)
        {
            if (force)
            {
                Instance._globalHider.raycastTarget = @in;
                var tempColor = Instance._globalHider.color;
                tempColor.a = @in ? 1f : 0f;
                Instance._globalHider.color = tempColor;
                return;
            }

            if (@in)
            {
                Instance._globalHider.raycastTarget = true;
                Instance._globalHider
                    .DOFade(1f, fadeTime)
                    .SetDelay(delay)
                    .SetUpdate(true);
            }
            else
            {
                Instance._globalHider
                    .DOFade(0f, fadeTime)
                    .SetDelay(delay)
                    .SetUpdate(true)
                    .OnComplete(() => Instance._globalHider.raycastTarget = false);
            }
        }
    }
}