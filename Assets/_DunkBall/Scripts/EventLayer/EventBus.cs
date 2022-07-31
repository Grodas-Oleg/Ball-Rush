using UnityEngine.Events;

namespace _DunkBall.Scripts.EventLayer
{
    public static class EventBus
    {
        public static UnityAction OnTotalScoreChange;
        public static UnityAction OnStarsChange;
        public static UnityAction OnNextBasketReached;

        static EventBus()
        {
        }

        public static void Clear()
        {
        }
    }
}