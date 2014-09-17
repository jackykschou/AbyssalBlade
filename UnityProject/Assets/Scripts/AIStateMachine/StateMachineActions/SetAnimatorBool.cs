using System;
using Assets.Scripts.GameScripts.GameLogic.Animator;
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
		    ObjectAnimator animator = stateMachine.owner.GetComponent<ObjectAnimator>();

		    if (animator == null)
		    {
		        throw new Exception("There is no Animator in the GameObject");
		    }

            animator.SetAnimatorBoolState(parameterName.Value);
		}

		public override void OnUpdate()
		{
		
		}
	}
}