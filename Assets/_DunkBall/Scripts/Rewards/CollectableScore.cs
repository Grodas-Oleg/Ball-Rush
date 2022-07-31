using _DunkBall.Scripts.Data;
using _DunkBall.Scripts.EventLayer;
using UnityEngine;

namespace _DunkBall.Scripts.Rewards
{
    public class CollectableScore : CollectableBase
    {
        protected override void OnEnter(Collider2D other)
        {
            EventBus.OnNextBasketReached?.Invoke();
            DataSaver.GlobalData.AddScore(1);
        }
    }
}