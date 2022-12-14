using System.Collections.Generic;
using _DunkBall.Scripts.Components;
using _DunkBall.Scripts.Rewards;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _DunkBall.Scripts
{
    public class BasketController : MonoBehaviour
    {
        private const float OUT_OFF_LEVEL_OFFSET = -2f;
        [Range(0, 100)] [SerializeField] private int _starSpawnChanche;
        [SerializeField] private CollectableScore _score;
        [SerializeField] private CollectableStar _star;
        [SerializeField] private TriggerComponent _ballTrigger;
        [SerializeField] private List<SpriteRenderer> _renderers;

        private void OnEnable()
        {
            _ballTrigger.AddCallbacks(OnEnter);
            _renderers.ForEach(spriteRenderer => spriteRenderer.color = Color.white);
            _score?.gameObject.SetActive(true);

            if (Random.Range(0, 100) > _starSpawnChanche) return;
            _star.gameObject.SetActive(true);
        }

        private void OnEnter(Collider2D other)
        {
            Ball.SetParent(transform);

            ReloadLevel.Instance.transform.position = new Vector3(0, transform.position.y + OUT_OFF_LEVEL_OFFSET);

            _renderers.ForEach(spriteRenderer => spriteRenderer.color = new Color(0.59f, 0.59f, 0.59f));

            if (transform.rotation != Quaternion.identity) transform.DORotateQuaternion(Quaternion.identity, .3f);
        }
    }
}