using _DunkBall.Scripts.EventLayer;
using _DunkBall.Scripts.Utilities;
using UnityEngine.SceneManagement;

namespace _DunkBall.Scripts.Components
{
    public class ReloadLevel : Singleton<ReloadLevel>
    {
        public static void Reload()
        {
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
            EventBus.Clear();
        }
    }
}