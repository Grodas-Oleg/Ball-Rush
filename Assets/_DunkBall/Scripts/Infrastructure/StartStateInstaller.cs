using _DunkBall.Scripts.Core;
using UnityEngine;
using Zenject;

namespace _DunkBall.Scripts.Infrastructure
{
    public class StartStateInstaller : MonoInstaller
    {
        [SerializeField] private Transform _startPoint;
        [SerializeField] private GameObject _ballPrefab;

        public override void InstallBindings()
        {
            BindBall();
        }

        private void BindBall()
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