using System;
using System.Collections.Generic;
using _DunkBall.Scripts.Components;
using _DunkBall.Scripts.EventLayer;
using Lean.Pool;
using UnityEngine;
using Zenject;
using static _DunkBall.Scripts.Core.BasketPosition;
using Random = UnityEngine.Random;

namespace _DunkBall.Scripts.Core
{
    public class BasketSpawner : MonoBehaviour
    {
        [SerializeField] private LeanGameObjectPool _basketPool;
        [SerializeField] private BasketMarker _startBasketMarker;
        [SerializeField] private List<BasketMarker> _markersContainer;
        [SerializeField] private List<GameObject> _basketControllers;

        private List<BasketPosition> _freePositions;
        private BasketPosition _lastBasketPosition;
        [Inject] private Ball _ball;

        protected void Awake()
        {
            _freePositions = new List<BasketPosition>
            {
                Middle,
                Right
            };

            EventBus.OnNextBasketReached += SpawnRandomBasket;

            SpawnStartBasket();
            SpawnRandomBasket();
        }

        private void SpawnStartBasket()
        {
            var transformPosition = _startBasketMarker.transform.position;
            transformPosition.z = 0;

            var startBasket = _basketPool.Spawn(transformPosition, Quaternion.identity);
            startBasket.GetComponent<BasketController>().DisableRewards();

            _ball.transform.SetParent(startBasket.transform);
            _ball.transform.position = new Vector3(_ball.transform.position.x, _ball.transform.position.y, 0);

            _lastBasketPosition = _startBasketMarker.BasketPosition;
            _basketControllers.Add(startBasket);
        }

        private void SpawnRandomBasket()
        {
            var randomFreeMarker = GetRandomFreeMarker();

            if (randomFreeMarker == null) Debug.Log(randomFreeMarker + "is Null");

            var markerPosition = (Vector2) randomFreeMarker.transform.position;
            var basket = _basketPool.Spawn(markerPosition, randomFreeMarker.transform.rotation);

            _basketControllers.Add(basket);

            if (_basketControllers.Count <= 2) return;

            var lastBasket = _basketControllers[0];
            lastBasket.GetComponent<BasketController>().ResetBasket();
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

        private void OnDestroy() => EventBus.OnNextBasketReached -= SpawnRandomBasket;
    }

    [Serializable]
    public enum BasketPosition
    {
        Left,
        Middle,
        Right
    }
}