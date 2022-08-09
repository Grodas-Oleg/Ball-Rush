using _DunkBall.Scripts.Components;
using UnityEngine;

namespace _DunkBall.Scripts.Collectables
{
    public class CollectableBase : MonoBehaviour
    {
        [SerializeField] protected TriggerComponent _trigger;

        protected virtual void Start() => _trigger.AddCallbacks(OnEnter);

        protected virtual void OnEnter(Collider2D other)
        {
        }
    }
}