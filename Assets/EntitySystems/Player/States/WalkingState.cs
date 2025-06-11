using System.Collections.Generic;
using System.Linq;
using EntitySystems.Controllers;
using EntitySystems.Interfaces;
using EntitySystems.StatesSystem;
using EntitySystems.StatesSystem.Interfaces;
using UnityEngine;
using ZLinq;

namespace PlayerSystems.States
{
    public class WalkingState : IState
    {
        public IState Parent { get; }
        public List<IState> Children => new List<IState>();
        public IState DefaultSubState => null;

        private IInputSource _inputSource;
        private MovementController _movementController;

        public WalkingState(IState parent)
        {
            Parent = parent;
        }

        public void Enter(IState prevState, IStateContext context)
        {
            Debug.Log("Entering Walking State");

            _inputSource = context.InputSource;
            _movementController = context.Controllers.OfType<MovementController>().FirstOrDefault();
        }

        public void Update(float deltaTime)
        {
            Vector2 direction = _inputSource.GetInputDirection();
            
            _movementController.Move(direction);
        }

        public void Exit()
        {
            // _stateMachine.MovementController.Stop();
            Debug.Log("Exiting WalkingState");
        }

        public IState CheckTransitions()
        {
            if (_inputSource.GetInputDirection() == Vector2.zero)
            {
                return Parent.Children.OfType<IdleState>().FirstOrDefault();
            }
            return null;
        }
    }
}