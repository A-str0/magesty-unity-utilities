using System;
using System.Collections.Generic;
using EntitySystems.Interfaces;
using EntitySystems.StatesSystem;
using EntitySystems.StatesSystem.Interfaces;
using Mono.Cecil;
using PlayerSystems;
using PlayerSystems.States;
using UnityEngine;

namespace EntitySystems.Controllers
{
    public class StateMachineController : MonoBehaviour
    {
        [SerializeField] private IState _rootState;
        [SerializeField] private IInputSource _inputSource;

        [SerializeField] private List<IController> controllers = new List<IController>();

        private StateMachine _stateMachine;

        private void Start()
        {
            foreach (IController controller in GetComponents<IController>())
            {
                controllers.Add(controller);
            }

            _rootState = new RootState(); // TODO: remade this
            _inputSource = new PlayerInput(); // TODO: remade this

            _stateMachine = new StateMachine(_inputSource, controllers);
            _stateMachine.Initialize(_rootState);
        }

        private void Update()
        {
            _stateMachine.Update(Time.deltaTime);
        }
    }
}