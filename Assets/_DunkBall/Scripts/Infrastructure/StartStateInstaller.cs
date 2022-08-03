using _DunkBall.Scripts.Core;
using _DunkBall.Scripts.UI;
using UnityEngine;
using Zenject;

namespace _DunkBall.Scripts.Infrastructure
{
    public class StartStateInstaller : MonoInstaller
    {
        [SerializeField] private Ball _ballPrefab;
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Joystick _joystick;
        [SerializeField] private BasketSpawner _basketSpawner;
        [SerializeField] private GameUI _gameUI;

        public override void InstallBindings()
        {
            BindJoystick();
            BindBasketSpawner();
            BindGameUI();
            BindBall();
        }

        private void BindBasketSpawner() =>
            Container
                .Bind<BasketSpawner>()
                .FromInstance(_basketSpawner)
                .AsSingle();

        private void BindJoystick() =>
            Container
                .Bind<IJoystick>()
                .To<Joystick>()
                .FromInstance(_joystick)
                .AsSingle();

        private void BindGameUI() =>
            Container
                .Bind<GameUI>()
                .FromInstance(_gameUI)
                .AsSingle();

        private void BindBall()
        {
            var ball = Container.InstantiatePrefabForComponent<Ball>(
                _ballPrefab,
                _startPoint.position,
                Quaternion.identity,
                null
            );

            Container.Bind<Ball>().FromInstance(ball).AsSingle().NonLazy();
        }
    }
}