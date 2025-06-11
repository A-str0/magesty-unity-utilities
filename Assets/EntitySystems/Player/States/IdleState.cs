using System.Collections.Generic;
using EntitySystems.Interfaces;
using EntitySystems.StatesSystem.Interfaces;
using UnityEngine;
using ZLinq;

namespace PlayerSystems.States
{
    public class IdleState : IState
    {
        public IState Parent { get; }
        public List<IState> Children => new List<IState>(); // Листовое состояние
        public IState DefaultSubState => null;

        private IInputSource _inputSource;

        public IdleState(IState parent)
        {
            Parent = parent;
        }

        public void Enter(IState prevState, IStateContext context)
        {
            _inputSource = context.InputSource;
            Debug.Log("Entered IdleState");
        }

        public void Update(float deltaTime)
        {
            // В состоянии покоя движение не применяется
            // Можно добавить легкую анимацию "дыхания" через Animator
        }

        public void Exit()
        {
            Debug.Log("Exiting IdleState");
        }

        public IState CheckTransitions()
        {
            // Переход в WalkingState, если есть ввод направления
            Vector2 direction = _inputSource.GetInputDirection();
            if (direction != Vector2.zero)
            {
                return Parent.Children.AsValueEnumerable().OfType<WalkingState>().FirstOrDefault();
            }

            return null;
        }
    }
}