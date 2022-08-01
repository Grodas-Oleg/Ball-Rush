using UnityEngine.Events;

namespace _DunkBall.Scripts.EventLayer
{
    public static class EventBus
    {
        public static UnityAction OnNextBasketReached;
        public static UnityAction OnGameOver;
        public static State<bool> isFirstLaunch;

        static EventBus()
        {
            isFirstLaunch = new State<bool>();
        }

        public static void Clear()
        {
        }
    }
}