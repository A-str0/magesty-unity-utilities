using System.Collections.Generic;
using EntitySystems.Interfaces;
using EntitySystems.StatesSystem;
using EntitySystems.StatesSystem.Interfaces;
using UnityEngine;

namespace PlayerSystems.States
{
    public class RootState : IState
    {
        public IState Parent => null;
        public List<IState> Children { get; }
        public IState DefaultSubState { get; }

        private IStateContext _context;

        public RootState()
        {
            IState groundedState = new GroundedState(this);
            Children = new List<IState> { groundedState };
            DefaultSubState = groundedState;
        }

        public void Enter(IState prevState, IStateContext context)
        {
            Debug.Log("Entering Root State");

            _context = context;
        }

        public void Update(float deltaTime) { }
        public void Exit() { }
        public IState CheckTransitions() => null;
    }
}