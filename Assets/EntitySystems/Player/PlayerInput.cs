using System;
using EntitySystems.Interfaces;
using UnityEngine;

namespace PlayerSystems
{
    public class PlayerInput : IInputSource
    {
        // TODO: New input system 
        public Vector2 GetInputDirection() => new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        public bool GetJumpInput() => Input.GetKeyDown(KeyCode.Space);
        public bool GetAccelerationInput() => Input.GetKey(KeyCode.LeftShift);
        public bool GetCrouchInput() => Input.GetKey(KeyCode.LeftControl);
        public bool GetDashInput() => Input.GetKeyDown(KeyCode.LeftAlt);
    }
}
