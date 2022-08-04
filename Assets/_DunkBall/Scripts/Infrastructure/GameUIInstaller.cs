using _DunkBall.Scripts.UI;
using UnityEngine;
using Zenject;

namespace _DunkBall.Scripts.Infrastructure
{
    public class GameUIInstaller : MonoInstaller
    {
        [SerializeField] private GameUI _gameUI;

        public override void InstallBindings() =>
            Container
                .Bind<GameUI>()
                .FromInstance(_gameUI)
                .AsSingle();
    }
}