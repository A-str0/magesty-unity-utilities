using System.Collections.Generic;
using MagestyUtilities.EntitySystem.Interfaces;
using MagestyUtilities.EntitySystem.HSM.Interfaces;
using MagestyUtilities.EntitySystem.HSM;
using UnityEngine;

namespace MagestyUtilities.EntitySystem.Controllers
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

            // _rootState = new RootState(); // TODO: remade this
            // _inputSource = new PlayerInput(); // TODO: remade this

            _stateMachine = new StateMachine(_inputSource, controllers);
            _stateMachine.Initialize(_rootState);
        }

        private void Update()
        {
            _stateMachine.Update(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            _stateMachine.FixedUpdate();
        }
    }
}