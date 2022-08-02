using _DunkBall.Scripts.Audio;
using _DunkBall.Scripts.Components;
using _DunkBall.Scripts.Utilities;
using UnityEngine;

namespace _DunkBall.Scripts.Core
{
    public class Ball : MonoBehaviour
    {
        private const string COLLISION_SOUND = "energy";

        [SerializeField] private float _forcePower = 5f;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Trajectory _trajectory;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private TriggerComponent _basketTrigger;

        public Rigidbody2D Rigidbody => _rigidbody;
        public Trajectory Trajectory => _trajectory;
        public float ForcePower => _forcePower;
        public int CollisionCount { get; set; }
        public Vector2 Force { get; set; }
        private void Awake() => _basketTrigger.AddCallbacks(OnInBasket);

        private void OnInBasket(Collider2D other) => transform.SetParent(other.transform.parent);

        private void OnCollisionEnter2D(Collision2D other)
        {
            CollisionCount++;
            _particleSystem.Play();
            AudioHelper.PlaySoundByName(COLLISION_SOUND);
        }
    }
}