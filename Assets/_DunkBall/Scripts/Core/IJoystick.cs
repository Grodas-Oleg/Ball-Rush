using System;
using UnityEngine;

namespace _DunkBall.Scripts.Core
{
    public interface IJoystick
    {
        event Action<Vector2, float> OnJoystickDrag;
        event Action OnJoystickUp;
    }
}