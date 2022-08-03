using _DunkBall.Scripts.Core;
using UnityEngine;

namespace _DunkBall.Scripts.Components
{
    public class BasketMarker : MonoBehaviour
    {
        [SerializeField] private BasketPosition _basketPosition;
        public BasketPosition BasketPosition => _basketPosition;

        private void OnDrawGizmos()
        {
            Gizmos.color = _basketPosition switch
            {
                BasketPosition.Left => Color.green,
                BasketPosition.Middle => Color.yellow,
                BasketPosition.Right => Color.red,
                _ => Gizmos.color
            };
            Gizmos.DrawSphere(transform.position, 0.25f);
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(1, 0, 0));
            Gizmos.color = Color.white;
        }
    }
}