using _DunkBall.Scripts.Core;
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

            var data = DataSaver.GlobalData;
            data.CurrentScore += Ball.CollisionCount <= 0 ? 2 : 1;
            DataSaver.GlobalData = data;
        }
    }
}