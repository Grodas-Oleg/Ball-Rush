using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _DunkBall.Scripts.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class BaseView : MonoBehaviour
    {
        [SerializeField] protected float _fadeDuration = 0.5f;
        [SerializeField] protected CanvasGroup _canvasGroup;
        [SerializeField] protected Image _hider;
        [SerializeField] protected Button _hiderButton;

        protected Sequence _animationSequence;
        protected Tween _hiderTween;

        public virtual void Show(bool force = false)
        {
            _animationSequence?.Kill();

            _canvasGroup.alpha = 0f;

            HiderFade(true);

            if (force)
            {
                _canvasGroup.interactable = true;
                _canvasGroup.alpha = 1f;
                gameObject.SetActive(true);
                return;
            }

            _animationSequence = DOTween.Sequence()
                .AppendCallback(() => gameObject.SetActive(true))
                .Append(_canvasGroup.DOFade(1f, _fadeDuration))
                .AppendCallback(() => _canvasGroup.interactable = true)
                .SetUpdate(true);
        }

        public void Hide(bool force = false)
        {
            _animationSequence?.Kill();

            _canvasGroup.interactable = false;

            HiderFade(false);

            if (force)
            {
                _canvasGroup.alpha = 0f;
                gameObject.SetActive(false);
                return;
            }

            _animationSequence = DOTween.Sequence()
                .Append(_canvasGroup.DOFade(1f, 0f))
                .Append(_canvasGroup.DOFade(0f, _fadeDuration))
                .AppendCallback(() => gameObject.SetActive(false))
                .SetUpdate(true);
        }

        private void HiderFade(bool flag)
        {
            _hiderTween?.Kill();

            if (_hider != null)
            {
                _hider.raycastTarget = flag;
                _hiderTween = _hider.DOFade(flag ? 0.6f : 0f, _fadeDuration).SetUpdate(true)
                    .OnComplete(() =>
                    {
                        if (_hiderButton != null)
                            _hiderButton.interactable = flag;
                    });
            }
        }

        public virtual void Init(params object[] parameters)
        {
        }

        protected virtual void OnDisable()
        {
        }
    }
}