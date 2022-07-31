using _DunkBall.Scripts.Data;
using UnityEngine;

namespace _DunkBall.Scripts.Rewards
{
    public class CollectableStar : CollectableBase
    {
        protected override void OnEnter(Collider2D other) => DataSaver.GlobalData.AddStars(1);
    }
}