using _DunkBall.Scripts.Audio;
using _DunkBall.Scripts.Data;
using UnityEngine;

namespace _DunkBall.Scripts.Rewards
{
    public class CollectableStar : CollectableBase
    {
        private const string ADD_STAR = "collect-coin_2";

        protected override void OnEnter(Collider2D other)
        {
            var data = DataSaver.GlobalData;
            data.TotalStars++;
            DataSaver.GlobalData = data;

            AudioHelper.PlaySoundByName(ADD_STAR);
        }
    }
}