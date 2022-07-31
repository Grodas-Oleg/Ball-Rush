using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace _DunkBall.Scripts.Utilities
{
    public class Trajectory : MonoBehaviour
    {
        [SerializeField] private int _dotsNumber;
        [SerializeField] private SpriteRenderer _dotsPrefab;
        [SerializeField] private float _dotSpace, _dotsMinSize, _dotsMaxSize;

        private Vector2 _pos;
        private float timeStamp;
        private readonly List<SpriteRenderer> _dotsList = new List<SpriteRenderer>();
        public bool IsShowed { get; private set; }
        private void Awake() => Init();

        private void Start() => _dotsList.ForEach(spriteRenderer => spriteRenderer.DOFade(0, 0));

        private void Init()
        {
            _dotsPrefab.transform.localScale = Vector3.one * _dotsMaxSize;

            var scale = _dotsMaxSize;
            var scaleFactor = scale / _dotsNumber;
            for (int i = 0; i < _dotsNumber; i++)
            {
                var dot = Instantiate(_dotsPrefab, transform);
                _dotsList.Add(dot);
                dot.transform.localScale = Vector3.one * scale;
                if (scale > _dotsMinSize) scale -= scaleFactor;
            }
        }

        public void UpdateDots(Vector3 ballPos, Vector2 forceApplied)
        {
            timeStamp = _dotSpace;

            foreach (var dot in _dotsList)
            {
                _pos.x = ballPos.x + forceApplied.x * timeStamp;
                _pos.y = ballPos.y + forceApplied.y * timeStamp -
                         Physics2D.gravity.magnitude * timeStamp * timeStamp / 2f;

                dot.transform.position = _pos;
                timeStamp += _dotSpace;
            }
        }

        public void Fade(bool flag)
        {
            IsShowed = flag;
            _dotsList.ForEach(spriteRenderer => spriteRenderer.DOFade(flag ? 1 : 0, 0));
        }
    }
}