using UnityEngine;

namespace MagestyUtilities.EntitySystem.Interfaces
{
    public interface IInputSource
    {
        public abstract Vector2 GetInputDirection();
        public abstract bool GetJumpInput();
        public abstract bool GetDashInput();
        public abstract bool GetAccelerationInput();
        public abstract bool GetCrouchInput();
    }
}