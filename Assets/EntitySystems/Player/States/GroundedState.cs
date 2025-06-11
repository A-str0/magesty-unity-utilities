using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using EntitySystems.Interfaces;
using EntitySystems.StatesSystem;
using EntitySystems.StatesSystem.Interfaces;
using UnityEngine;
using ZLinq;

namespace PlayerSystems.States
{
    public class GroundedState : IState
    {
        public IState Parent { get; }
        public List<IState> Children { get; }
        public IState DefaultSubState { get; }

        private IInputSource _inputSource;

        public GroundedState(IState parent)
        {
            Parent = parent;

            var idleState = new IdleState(this);
            var walkingState = new WalkingState(this);
            Children = new List<IState> { idleState, walkingState };
            DefaultSubState = idleState;
        }

        public void Enter(IState prevState, IStateContext context)
        {
            _inputSource = context.InputSource;
            Debug.Log("Entered GroundedState");
        }

        public void Update(float deltaTime)
        {
            // Общая логика для всех наземных состояний (если нужно)
        }

        public void Exit()
        {
            Debug.Log("Exiting GroundedState");
        }

        public IState CheckTransitions()
        {
            // if (_inputSource.GetJumpInput())
            // {
            //     return Parent.Children.OfType<AirborneState>().FirstOrDefault();
            // }

            // Можно добавить проверку "упал с платформы" через MovementController.IsGrounded()
            // Но в вашем случае это не требуется, так как фокус на передвижении

            return null;
        }
    }
}