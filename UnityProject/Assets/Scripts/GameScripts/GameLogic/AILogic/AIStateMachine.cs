using StateMachine;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.AILogic
{
    [RequireComponent(typeof(StateMachine.StateMachineBehaviour))]
    public class AIStateMachine : GameLogic
    {
        private StateMachineBehaviour stateMachineBehaviour;

        protected override void Initialize()
        {
            base.Initialize();
            stateMachineBehaviour = GetComponent<StateMachineBehaviour>();
            StateMachine.StateMachine stateMachine = ScriptableObject.CreateInstance<StateMachine.StateMachine>();
            stateMachine.name = stateMachineBehaviour.stateMachine.name + "(Bind)";
            StateMachine.StateMachine.Copy(stateMachineBehaviour.stateMachine, stateMachine, false);
            stateMachineBehaviour.stateMachine = stateMachine;
            stateMachineBehaviour.SetDefaultState();
        }

        protected override void Deinitialize()
        {
        }
    }
}
