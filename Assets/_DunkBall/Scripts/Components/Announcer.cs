using _DunkBall.Scripts.Utilities;
using DG.Tweening;
using Lean.Pool;
using TMPro;
using UnityEngine;

namespace _DunkBall.Scripts.Components
{
    public class Announcer : Singleton<Announcer>
    {
        [SerializeField] private LeanGameObjectPool _textPool;

        public static void SpawnAnnounce(Vector2 position, params object[] data)
        {
            var text = Instance._textPool.Spawn(position, Quaternion.identity);
            // var tmp = text.GetComponent<TextMeshPro>();
            // if (!string.IsNullOrEmpty(data[0] as string))
            // {
            //     tmp.SetText(data.ToString());
            //     tmp.color = new Color(0.57f, 0f, 1f);
            // }
            // else
            // {
            //     tmp.SetText("+ " + data);
            //     tmp.color = new Color(1f, 0.26f, 0f);
            // }
            //

            DOTween.Sequence()
                .AppendCallback(() => text.transform.DOMoveY(text.transform.position.y + 1f, 5f))
                .AppendCallback(() => text.transform.DOScale(1.1f, 5f))
                .SetUpdate(true)
                .OnComplete(() => Instance._textPool.Despawn(text));
        }
    }
}