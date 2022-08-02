using System.Collections;
using _DunkBall.Scripts.Core;
using UnityEngine;

namespace _DunkBall.Scripts.Components
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float _offset;
        [SerializeField] private float _smoothSpeed = .1f;
        private Transform _targetTransform => BallController.BallTransform;
        private void Start() => StartCoroutine(FollowTarget());

        private IEnumerator FollowTarget()
        {
            while (enabled)
            {
                var position = transform.position;
                position.y = _targetTransform.position.y + _offset;
                var smoothPos = Vector3.Lerp(transform.position, position, _smoothSpeed);
                transform.position = smoothPos;

                yield return null;
            }
        }
    }
}