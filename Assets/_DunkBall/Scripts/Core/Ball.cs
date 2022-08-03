using System.Collections;
using _DunkBall.Scripts.Audio;
using _DunkBall.Scripts.Components;
using _DunkBall.Scripts.Utilities;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _DunkBall.Scripts.Core
{
    public class Ball : MonoBehaviour
    {
        private const string COLLISION_SOUND = "energy";
        private const float DRAG_DISTANCE_THRESHOLD = .75f;
        private const float UNLOCK_DELAY = .3f;
        private const float MAX_DRAG_DISTANCE = 2f;

        [SerializeField] private float _forcePower = 5f;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Trajectory _trajectory;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private TriggerComponent _basketTrigger;

        private IJoystick _joystick;
        private float _distance;
        private Vector2 _force;
        private bool _isLocked;
        public int CollisionCount { get; private set; }

        [Inject]
        private void Construct(IJoystick joystick)
        {
            _joystick = joystick;
            _joystick.OnJoystickDrag += OnJoystickDrag;
            _joystick.OnJoystickUp += OnJoystickUp;
        }
        private void Awake() => _basketTrigger.AddCallbacks(OnInBasket);
        private void OnInBasket(Collider2D other) => transform.SetParent(other.transform.parent);

        private void OnJoystickDrag(Vector2 direction, float distance)
        {
            if (_isLocked) return;

            if (distance > MAX_DRAG_DISTANCE) distance = MAX_DRAG_DISTANCE;

            _distance = distance;
            _rigidbody.isKinematic = true;

            var rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotation);
            if (transform.parent != null) transform.parent.rotation = Quaternion.Euler(0f, 0f, rotation - 90);

            if (_distance <= DRAG_DISTANCE_THRESHOLD)
            {
                _trajectory.Fade(false);
                return;
            }

            if (!_trajectory.IsShowed) _trajectory.Fade(true);

            _force = direction * _distance * _forcePower;

            _trajectory.UpdateDots(transform.position, _force);
        }

        private void OnJoystickUp()
        {
            if (_isLocked || _distance <= DRAG_DISTANCE_THRESHOLD) return;

            _isLocked = true;

            _trajectory.Fade(false);
            transform.SetParent(null);
            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(_force, ForceMode2D.Impulse);

            DOVirtual.DelayedCall(.3f, () => CollisionCount = 0);

            StartCoroutine(CheckLock());
        }

        private IEnumerator CheckLock()
        {
            while (!_rigidbody.IsSleeping())
                yield return null;


            yield return new WaitForSeconds(UNLOCK_DELAY);
            _isLocked = false;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            CollisionCount++;
            _particleSystem.Play();
            AudioHelper.PlaySoundByName(COLLISION_SOUND);
        }

        private void OnDestroy()
        {
            _joystick.OnJoystickDrag -= OnJoystickDrag;
            _joystick.OnJoystickUp -= OnJoystickUp;
        }
    }
}