using UnityEngine;
using UnityEngine.Events;

namespace _DunkBall.Scripts.Components
{
    public class CollisionComponent : MonoBehaviour
    {
        [SerializeField] private UnityEvent<Collision2D> _onEnter;

        public void AddCallbacks(UnityAction<Collision2D> onEnter = null)
        {
            if (onEnter != null)
                _onEnter.AddListener(onEnter);
        }

        private void OnCollisionEnter2D(Collision2D other) => _onEnter?.Invoke(other);
    }
}