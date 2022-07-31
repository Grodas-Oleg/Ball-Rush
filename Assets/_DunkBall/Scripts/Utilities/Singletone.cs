using UnityEngine;

namespace _DunkBall.Scripts.Utilities
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance;

        [SerializeField] private bool IsNeedToDontDestroy;
        protected void Awake()
        {
            if (Instance == null || !IsNeedToDontDestroy)
            {
                Instance = this as T;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            if (Application.isPlaying && IsNeedToDontDestroy)
                DontDestroyOnLoad(this);

            OnAwake();
        }

        protected virtual void OnAwake() { }

#if UNITY_EDITOR
        private void Reset()
        {
            name = IsNeedToDontDestroy ? $"{GetType().Name}_Slton" : $"{GetType().Name}";
        }
#endif
    }
}