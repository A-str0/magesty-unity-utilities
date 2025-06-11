using EntitySystems.StatesSystem;
using EntitySystems.Interfaces;
using System.Collections.Generic;

namespace EntitySystems.StatesSystem.Interfaces
{
    public interface IState
    {
        public IState Parent { get; }
        public List<IState> Children { get; }
        public IState DefaultSubState { get; }

        public abstract void Enter(IState prevState, IStateContext context);
        public abstract void Update(float deltaTime);
        public abstract void Exit();
        public abstract IState CheckTransitions();
    }
}