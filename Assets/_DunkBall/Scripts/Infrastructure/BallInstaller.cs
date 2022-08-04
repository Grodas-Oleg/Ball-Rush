using _DunkBall.Scripts.Core;
using UnityEngine;
using Zenject;

namespace _DunkBall.Scripts.Infrastructure
{
    public class BallInstaller : MonoInstaller
    {
        [SerializeField] private Ball _ballPrefab;
        [SerializeField] private Transform _startPoint;

        public override void InstallBindings()
        {
            var ball = Container.InstantiatePrefabForComponent<Ball>(
                _ballPrefab,
                _startPoint.position,
                Quaternion.identity,
                null
            );

            Container.Bind<Ball>().FromInstance(ball).AsSingle();
        }
    }
}