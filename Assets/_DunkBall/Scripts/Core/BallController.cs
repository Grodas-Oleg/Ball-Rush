using System.Collections;
using _DunkBall.Scripts.Audio;
using _DunkBall.Scripts.Components;
using _DunkBall.Scripts.Utilities;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _DunkBall.Scripts.Core
{
    public class BallController : Singleton<BallController>, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        private const float UNLOCK_DELAY = .3f;
        private const float DRAG_DISTANCE_THRESHOLD = .75f;
        private const string COLLISION_SOUND = "energy";

        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _forcePower;
        [SerializeField] private float _maxDragDistance;
        [SerializeField] private Trajectory _trajectory;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private Transform _basketTransform;
        [SerializeField] private Transform _ballTransform;
        [SerializeField] private CollisionComponent _collision;
        public static Transform BallTransform => Instance._ballTransform;
        private Camera _camera => Camera.main;
        private Vector2 _startPoint;
        private Vector2 _endPoint;
        private Vector2 _direction;
        private Vector2 _force;
        private float _distance;
        private bool _isLocked;
        private int _collisionCount;
        public static int CollisionCount => Instance._collisionCount;

        private void Start()
        {
            RigidBodyStatus(true);
            _collision.AddCallbacks(OnEnter);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_isLocked) return;

            RigidBodyStatus(true);

            _endPoint = _camera.ScreenToWorldPoint(Input.mousePosition);
            _distance = Vector2.Distance(_startPoint, _endPoint);
            _direction = (_startPoint - _endPoint).normalized;

            if (_basketTransform != null)
            {
                var rotation = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
                _basketTransform.rotation = Quaternion.Euler(0f, 0f, rotation - 90);
                _ballTransform.rotation = Quaternion.Euler(0f, 0f, rotation);
            }

            if (_distance > _maxDragDistance) _distance = _maxDragDistance;

            if (_distance <= DRAG_DISTANCE_THRESHOLD)
            {
                _trajectory.Fade(false);
                return;
            }

            if (!_trajectory.IsShowed) _trajectory.Fade(true);

            _trajectory.UpdateDots(_ballTransform.position, _force);
            _force = _direction * _distance * _forcePower;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_isLocked) return;

            if (_distance <= DRAG_DISTANCE_THRESHOLD) return;

            _trajectory.Fade(false);
            _isLocked = true;
            _ballTransform.SetParent(null);

            RigidBodyStatus(false);
            Push(_force);

            DOVirtual.DelayedCall(.3f, () => _collisionCount = 0);

            StartCoroutine(CheckLock());
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _startPoint = _camera.ScreenToWorldPoint(Input.mousePosition);
        }

        public static void SetParent(Transform parent)
        {
            Instance._ballTransform.SetParent(parent);
            Instance._basketTransform = parent;
        }

        private void Push(Vector2 force) => _rigidbody.AddForce(force, ForceMode2D.Impulse);
        private void RigidBodyStatus(bool flag) => _rigidbody.isKinematic = flag;

        private IEnumerator CheckLock()
        {
            while (!_rigidbody.IsSleeping())
                yield return null;

            yield return new WaitForSeconds(UNLOCK_DELAY);
            _isLocked = false;
        }

        private void OnEnter(Collision2D other)
        {
            _collisionCount++;
            _particleSystem.Play();
            AudioHelper.PlaySoundByName(COLLISION_SOUND);
        }
    }
}