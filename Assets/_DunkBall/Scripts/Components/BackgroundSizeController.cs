using UnityEngine;

namespace _DunkBall.Scripts.Components
{
    public class BackgroundSizeController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        void Awake()
        {
            var cameraHeight = Camera.main.orthographicSize * 2;
            var cameraSize = new Vector2(Camera.main.aspect * cameraHeight, cameraHeight);
            var spriteSize = spriteRenderer.sprite.bounds.size;

            Vector2 scale = transform.localScale;
            if (cameraSize.x >= cameraSize.y)
                scale *= cameraSize.x / spriteSize.x;
            else
                scale *= cameraSize.y / spriteSize.y;

            transform.position = Vector2.zero;
            transform.localScale = scale;
        }
    }
}