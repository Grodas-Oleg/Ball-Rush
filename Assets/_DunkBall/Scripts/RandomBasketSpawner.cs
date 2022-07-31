using System;
using System.Collections.Generic;
using System.Linq;
using _DunkBall.Scripts.EventLayer;
using _DunkBall.Scripts.Utilities;
using Lean.Pool;
using UnityEngine;
using static _DunkBall.Scripts.BasketPosition;
using Random = UnityEngine.Random;

namespace _DunkBall.Scripts
{
    public class RandomBasketSpawner : Singleton<RandomBasketSpawner>
    {
        [SerializeField] private List<BasketTransformContainer> _transformContainers;
        [SerializeField] private LeanGameObjectPool _basketPool;
        [SerializeField] private List<BasketPosition> _freePositions;
        [SerializeField] private BasketPosition _lastBasketPosition = Left;
        [SerializeField] private List<GameObject> _basketControllers;

        private void Start()
        {
            _freePositions = new List<BasketPosition>
            {
                Center,
                Right
            };

            SpawnBasket();

            EventBus.OnNextBasketReached += SpawnBasket;
        }

        private void SpawnBasket()
        {
            var randomTransform = GetRandomTransformByPosition();

            var randomTransformPosition = randomTransform.position;
            randomTransformPosition.z = 0;

            var basket = _basketPool.Spawn(randomTransformPosition, randomTransform.rotation);

            _basketControllers.Add(basket);

            if (_basketControllers.Count <= 2) return;

            var lastBasket = _basketControllers[0];
            _basketControllers.Remove(lastBasket);
            _basketPool.Despawn(lastBasket);
        }

        private Transform GetRandomTransformByPosition()
        {
            BasketPosition randomPosition = _freePositions[Random.Range(0, 1)];

            foreach (var container in _transformContainers.Where(
                container => container.BasketPosition == randomPosition))
            {
                _freePositions.Add(_lastBasketPosition);
                _lastBasketPosition = randomPosition;
                _freePositions.Remove(randomPosition);
                return container.GetRandomTransform();
            }

            return default;
        }
    }

    [Serializable]
    public class BasketTransformContainer
    {
        [SerializeField] private BasketPosition _basketPosition;
        [SerializeField] private List<Transform> _points;

        public Transform GetRandomTransform() => _points[Random.Range(0, _points.Count)];

        public BasketPosition BasketPosition => _basketPosition;
        public List<Transform> Points => _points;
    }

    [Serializable]
    public enum BasketPosition
    {
        Left,
        Center,
        Right
    }
}