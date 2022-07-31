using UnityEngine;
using UnityEngine.Events;

namespace _DunkBall.Scripts.Components
{
    public class TriggerComponent : MonoBehaviour
    {
        [SerializeField] private bool _isDisableOnEnter;
        [SerializeField] private bool _isDisableOnExit;

        [SerializeField] private UnityEvent<Collider2D> _onEnter;
        [SerializeField] private UnityEvent<Collider2D> _onExit;

        public void AddCallbacks(UnityAction<Collider2D> onEnter = null, UnityAction<Collider2D> onExit = null)
        {
            if (onEnter != null)
                _onEnter.AddListener(onEnter);

            if (onExit != null)
                _onExit.AddListener(onExit);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _onEnter?.Invoke(other);
            if (_isDisableOnEnter)
                gameObject.SetActive(false);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _onExit?.Invoke(other);
            if (_isDisableOnExit)
                gameObject.SetActive(false);
        }
    }
}