using System;
using Assets.Scripts.Constants;
using Assets.Scripts.Utility;
using StateMachine;
using StateMachine.Action;

namespace Assets.Scripts.AIStateMachine.StateMachineActions
{
	[Info (category = "Custom",
	description = "Enemy standing still in idle animation", 
	url = "")]
    [Serializable]
	public class SetAnimatorBool : StateAction
	{
        [FieldInfo(tooltip = "Name of the parameter of the animator")]
        public StringParameter parameterName;

		public override void OnEnter()
		{
		    stateMachine.owner.TriggerGameScriptEvent(GameScriptEvent.SetAnimatorBoolState, parameterName.Value);
		}

		public override void OnUpdate()
		{
		
		}
	}
}