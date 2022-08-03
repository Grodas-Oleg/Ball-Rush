using System;
using System.Collections.Generic;
using _DunkBall.Scripts.Components;
using _DunkBall.Scripts.EventLayer;
using Lean.Pool;
using UnityEngine;
using static _DunkBall.Scripts.Core.BasketPosition;
using Random = UnityEngine.Random;

namespace _DunkBall.Scripts.Core
{
    public class RandomBasketSpawner : MonoBehaviour
    {
        [SerializeField] private LeanGameObjectPool _basketPool;
        [SerializeField] private GameObject _startBasket;
        [SerializeField] private List<BasketMarker> _markersContainer;
        [SerializeField] private List<GameObject> _basketControllers;

        private List<BasketPosition> _freePositions;
        private BasketPosition _lastBasketPosition = Left;

        protected void Awake()
        {
            _freePositions = new List<BasketPosition>
            {
                Middle,
                Right
            };

            SpawnBasket();

            EventBus.OnNextBasketReached += SpawnBasket;
        }

        private void SpawnBasket()
        {
            var randomFreeMarker = GetRandomFreeMarker();

            if (randomFreeMarker == null) Debug.Log(randomFreeMarker + "is Null");

            var markerPosition = randomFreeMarker.transform.position;
            markerPosition.z = 0;

            var basket = _basketPool.Spawn(markerPosition, randomFreeMarker.transform.rotation);

            _basketControllers.Add(basket);

            if (_basketControllers.Count <= 2) return;

            if (_startBasket != null) Destroy(_startBasket);

            var lastBasket = _basketControllers[0];
            _basketControllers.Remove(lastBasket);
            _basketPool.Despawn(lastBasket);
        }

        private BasketMarker GetRandomFreeMarker()
        {
            BasketPosition randomPosition = _freePositions[Random.Range(0, 2)];

            var markers = _markersContainer.FindAll(marker => marker.BasketPosition == randomPosition);

            _freePositions.Add(_lastBasketPosition);
            _lastBasketPosition = randomPosition;
            _freePositions.Remove(randomPosition);

            return markers[Random.Range(0, markers.Count)];
        }

        private void OnDestroy() => EventBus.OnNextBasketReached -= SpawnBasket;
    }

    [Serializable]
    public enum BasketPosition
    {
        Left,
        Middle,
        Right
    }
}