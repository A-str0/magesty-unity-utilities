using UnityEngine;

namespace MagestyUtilities.EntitySystem.Interfaces
{
    public interface IMovementController
    {
        void Move(Vector2 direction);
        void Stop();
        // TODO: Different surfaces
        // void SetSurface();
    }
}