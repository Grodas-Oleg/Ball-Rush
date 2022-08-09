using _DunkBall.Scripts.Audio;
using _DunkBall.Scripts.Components;
using _DunkBall.Scripts.Core;
using _DunkBall.Scripts.Data;
using _DunkBall.Scripts.EventLayer;
using DG.Tweening;
using UnityEngine;

namespace _DunkBall.Scripts.Collectables
{
    public class CollectableScore : CollectableBase
    {
        private const string ADD_SCORE = "collect-coin_1";

        protected override void OnEnter(Collider2D other)
        {
            if (!other.TryGetComponent(out Ball ball)) return;

            EventBus.OnNextBasketReached?.Invoke();

            var data = DataSaver.GlobalData;

            int value;
            if (ball.CollisionCount <= 0)
            {
                Announcer.SpawnAnnounce(transform.position, "perfect");
                value = 2;
            }
            else
                value = 1;


            data.CurrentScore += value;
            if (data.CurrentScore > data.MaxReachedScore) data.MaxReachedScore = data.CurrentScore;
            DataSaver.GlobalData = data;

            DOVirtual.DelayedCall(.3f, () =>
            {
                Announcer.SpawnAnnounce(transform.position, value);
                AudioHelper.PlaySoundByName(ADD_SCORE);
            });
        }
    }
}