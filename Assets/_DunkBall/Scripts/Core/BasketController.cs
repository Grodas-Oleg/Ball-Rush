using System.Collections.Generic;
using _DunkBall.Scripts.Collectables;
using _DunkBall.Scripts.Components;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _DunkBall.Scripts.Core
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
            _star.gameObject.SetActive(Random.Range(0, 100) < _starSpawnChanche);
        }

        public void DisableRewards()
        {
            _score.gameObject.SetActive(false);
            _star.gameObject.SetActive(false);
            _ballTrigger.gameObject.SetActive(false);
        }

        private void OnEnter(Collider2D other)
        {
            ReloadLevel.Instance.transform.position = new Vector3(0, transform.position.y + OUT_OFF_LEVEL_OFFSET);
            if (transform.rotation != Quaternion.identity) transform.DORotateQuaternion(Quaternion.identity, .3f);

            if (_score == null) return;
            _renderers.ForEach(spriteRenderer => spriteRenderer.color = new Color(0.59f, 0.59f, 0.59f));
        }

        public void OnDisable()
        {
            _score.gameObject.SetActive(true);
            _ballTrigger.gameObject.SetActive(true);
        }
    }
}