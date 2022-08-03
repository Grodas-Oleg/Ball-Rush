using _DunkBall.Scripts.Core;
using UnityEngine;
using Zenject;

namespace _DunkBall.Scripts.Infrastructure
{
    public class StartStateInstaller : MonoInstaller
    {
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Ball _ballPrefab;
        [SerializeField] private Joystick _joystick;

        public override void InstallBindings()
        {
            BindJoystick();
            BindBall();
        }

        private void BindJoystick() =>
            Container
                .Bind<IJoystick>()
                .To<Joystick>()
                .FromInstance(_joystick)
                .AsSingle();

        private void BindBall()
        {
            var ball = Container.InstantiatePrefabForComponent<Ball>(
                _ballPrefab,
                _startPoint.position,
                Quaternion.identity,
                _startPoint
            );

            Container.Bind<Ball>().FromInstance(ball).AsSingle();
        }
    }
}