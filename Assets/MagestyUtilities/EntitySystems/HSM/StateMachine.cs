using System.Collections.Generic;
using MagestyUtilities.EntitySystem.Interfaces;
using MagestyUtilities.EntitySystem.HSM.Interfaces;
using UnityEngine;

namespace MagestyUtilities.EntitySystem.HSM
{
    public class StateMachine : IStateContext
    {
        private IState _rootState;

        private IInputSource _inputSource;
        public IInputSource InputSource => _inputSource;

        private List<IController> _controllers;
        public IReadOnlyList<IController> Controllers => _controllers;

        private List<IState> _statesPath = new List<IState>();

        public IState RootState => _rootState;

        public StateMachine(IInputSource inputSource, List<IController> controllers)
        {
            _inputSource = inputSource;
            _controllers = controllers;
        }

        public void Initialize(IState rootState)
        {
            _rootState = rootState;
            _statesPath = GetInitialPath(rootState);
            foreach (var state in _statesPath)
            {
                state.Enter(null, this);
            }
        }

        private List<IState> GetInitialPath(IState state)
        {
            var path = new List<IState>();
            while (state != null)
            {
                path.Add(state);
                state = state.DefaultSubState;
            }
            return path;
        }

        private IState GetLeafState(IState state)
        {
            while (state.DefaultSubState != null)
            {
                state = state.DefaultSubState;
            }
            return state;
        }

        private List<IState> GetPath(IState state)
        {
            var path = new List<IState>();
            while (state != null)
            {
                path.Insert(0, state);
                state = state.Parent;
            }
            return path;
        }

        private void PerformTransition(IState targetState)
        {
            IState targetLeaf = GetLeafState(targetState);
            List<IState> targetPath = GetPath(targetLeaf);
            List<IState> currentPathCopy = new List<IState>(_statesPath);

            int i = 0;
            while (i < currentPathCopy.Count && i < targetPath.Count && currentPathCopy[i] == targetPath[i])
            {
                i++;
            }

            for (int j = currentPathCopy.Count - 1; j >= i; j--)
            {
                currentPathCopy[j].Exit();
            }

            IState transitionSource = currentPathCopy[currentPathCopy.Count - 1];
            for (int j = i; j < targetPath.Count; j++)
            {
                targetPath[j].Enter(transitionSource, this);
                transitionSource = targetPath[j];
            }

            _statesPath = targetPath;
        }



        public void Update(float deltaTime)
        {
            foreach (var state in _statesPath)
            {
                state.Update(deltaTime);
            }

            CheckTransitions();
        }

        public void FixedUpdate()
        {
            foreach (var state in _statesPath)
            {
                state.FixedUpdate();
            }

            CheckTransitions();
        }

        private void CheckTransitions()
        {
            if (_statesPath.Count == 0)
            {
                // If there are no states, we're in a "dead" state, so we just
                // return without doing anything.
                return;
            }

            IState currentLeaf = _statesPath[_statesPath.Count - 1];
            IState targetState = currentLeaf.CheckTransitions();
            if (targetState != null && targetState != currentLeaf)
            {
                PerformTransition(targetState);
            }
        }
    }
}
