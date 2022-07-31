using System.Collections;
using UnityEngine;

namespace _DunkBall.Scripts.Components
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float _offsetTopY;
        [SerializeField] private float _offsetBottomY;
        [SerializeField] private float _smoothSpeed = .1f;
        private Transform _targetTransform => Ball.Instance.transform;

        private void Start()
        {
            StartCoroutine(FollowTarget());
        }

        private IEnumerator FollowTarget()
        {
            while (enabled)
            {
                Vector3 position = transform.position;
                if (position.x < _offsetTopY)
                    position.y = _targetTransform.position.y + _offsetTopY;
                else if (position.x > _offsetBottomY)
                    position.y = _targetTransform.position.y + _offsetBottomY;
                else
                    position.y = _targetTransform.position.y;

                var smoothPos = Vector3.Lerp(transform.position, position, _smoothSpeed);
                transform.position = smoothPos;

                yield return null;
            }
        }
    }
}