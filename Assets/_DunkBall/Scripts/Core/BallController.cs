using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace _DunkBall.Scripts.Core
{
    public class BallController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        private const float UNLOCK_DELAY = .3f;
        private const float DRAG_DISTANCE_THRESHOLD = .75f;
        private Camera _camera => Camera.main;
        private Vector2 _startPoint;
        private Vector2 _endPoint;
        private Vector2 _direction;
        private float _maxDragDistance = 2f;
        private float _distance;
        private bool _isLocked;

        [Inject]
        private void Construct(Ball ball) => _ball = ball;

        private Ball _ball;

        private void Start() => _ball.Rigidbody.isKinematic = true;

        public void OnDrag(PointerEventData eventData)
        {
            if (_isLocked) return;

            _ball.Rigidbody.isKinematic = true;

            _endPoint = _camera.ScreenToWorldPoint(Input.mousePosition);
            _distance = Vector2.Distance(_startPoint, _endPoint);
            _direction = (_startPoint - _endPoint).normalized;

            if (_ball.transform.parent != null)
            {
                var rotation = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
                _ball.transform.parent.rotation = Quaternion.Euler(0f, 0f, rotation - 90);
                _ball.transform.rotation = Quaternion.Euler(0f, 0f, rotation);
            }

            if (_distance > _maxDragDistance) _distance = _maxDragDistance;

            if (_distance <= DRAG_DISTANCE_THRESHOLD)
            {
                _ball.Trajectory.Fade(false);
                return;
            }

            if (!_ball.Trajectory.IsShowed) _ball.Trajectory.Fade(true);

            _ball.Trajectory.UpdateDots(_ball.transform.position, _ball.Force);
            _ball.Force = _direction * _distance * _ball.ForcePower;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_isLocked) return;

            if (_distance <= DRAG_DISTANCE_THRESHOLD) return;

            _isLocked = true;

            _ball.Trajectory.Fade(false);
            _ball.transform.SetParent(null);
            _ball.Rigidbody.isKinematic = false;
            _ball.Rigidbody.AddForce(_ball.Force, ForceMode2D.Impulse);

            DOVirtual.DelayedCall(.3f, () => _ball.CollisionCount = 0);

            StartCoroutine(CheckLock());
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _startPoint = _camera.ScreenToWorldPoint(Input.mousePosition);
        }

        private IEnumerator CheckLock()
        {
            while (!_ball.Rigidbody.IsSleeping())
                yield return null;

            yield return new WaitForSeconds(UNLOCK_DELAY);
            _isLocked = false;
        }
    }
}