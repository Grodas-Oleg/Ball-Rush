using _DunkBall.Scripts.Audio;
using _DunkBall.Scripts.Core;
using _DunkBall.Scripts.Data;
using UnityEngine;

namespace _DunkBall.Scripts.Collectables
{
    public class CollectableStar : CollectableBase
    {
        private const string ADD_STAR = "collect-coin_2";

        protected override void OnEnter(Collider2D other)
        {
            if (!other.TryGetComponent(out Ball ball)) return;

            var data = DataSaver.GlobalData;
            data.TotalStars++;
            DataSaver.GlobalData = data;

            AudioHelper.PlaySoundByName(ADD_STAR);
        }
    }
}