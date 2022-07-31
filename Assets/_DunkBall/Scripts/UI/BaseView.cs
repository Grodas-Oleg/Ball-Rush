using System;
using DG.Tweening;
using UnityEngine;

namespace _DunkBall.Scripts.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class BaseView : MonoBehaviour
    {
        [SerializeField] protected float _fadeDuration = 0.5f;
        [SerializeField] protected CanvasGroup _canvasGroup;

        protected Sequence _animationSequence;

        public virtual void Show(bool force = false)
        {
            if (_animationSequence != null)
                _animationSequence.Kill();

            _canvasGroup.alpha = 0f;

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
            if (_animationSequence != null)
                _animationSequence.Kill();

            _canvasGroup.interactable = false;

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

        public virtual void Init(params object[] parameters)
        {
        }

        protected virtual void OnDisable()
        {
        }
    }
}