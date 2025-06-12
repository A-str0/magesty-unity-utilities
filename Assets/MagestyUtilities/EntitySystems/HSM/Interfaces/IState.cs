using MagestyUtilities.EntitySystem.HSM;
using MagestyUtilities.EntitySystem.Interfaces;
using System.Collections.Generic;

namespace MagestyUtilities.EntitySystem.HSM.Interfaces
{
    public interface IState
    {
        public IState Parent { get; }
        public List<IState> Children { get; }
        public IState DefaultSubState { get; }

        public abstract void Enter(IState prevState, IStateContext context);
        public abstract void Update(float deltaTime);
        public abstract void FixedUpdate();
        public abstract void Exit();
        public abstract IState CheckTransitions();
    }
}