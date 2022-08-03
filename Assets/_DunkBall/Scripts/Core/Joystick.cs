using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _DunkBall.Scripts.Core
{
    public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IJoystick
    {
        private Camera _camera => Camera.main;
        private Vector2 _startPoint;
        private Vector2 _endPoint;
        private Vector2 _direction;
        private float _distance;

        public event Action<Vector2, float> OnJoystickDrag;
        public event Action OnJoystickUp;

        public void OnDrag(PointerEventData eventData)
        {
            _endPoint = _camera.ScreenToWorldPoint(Input.mousePosition);
            _distance = Vector2.Distance(_startPoint, _endPoint);
            _direction = (_startPoint - _endPoint).normalized;

            OnJoystickDrag?.Invoke(_direction, _distance);
        }

        public void OnPointerUp(PointerEventData eventData) => OnJoystickUp?.Invoke();

        public void OnPointerDown(PointerEventData eventData) =>
            _startPoint = _camera.ScreenToWorldPoint(Input.mousePosition);
    }
}