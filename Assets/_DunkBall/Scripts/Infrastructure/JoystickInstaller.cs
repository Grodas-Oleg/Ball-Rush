using _DunkBall.Scripts.Core;
using UnityEngine;
using Zenject;

namespace _DunkBall.Scripts.Infrastructure
{
    public class JoystickInstaller : MonoInstaller
    {
        [SerializeField] private Joystick _joystick;

        public override void InstallBindings()
        {
            Container
                .Bind<IJoystick>()
                .To<Joystick>()
                .FromInstance(_joystick)
                .AsSingle();
        }
    }
}