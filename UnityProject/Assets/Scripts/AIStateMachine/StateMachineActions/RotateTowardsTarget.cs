using Assets.Scripts.Constants;
using Assets.Scripts.Utility;
using StateMachine.Action;

namespace Assets.Scripts.AIStateMachine.StateMachineActions
{
	[Info (category = "Custom",
    description = "Rotate towards target of the skill caster", 
	url = "Link")]
	public class RotateTowardsTarget : StateAction
	{
		public override void OnEnter()
		{
		
		}

		public override void OnUpdate()
		{
            stateMachine.owner.TriggerGameScriptEvent(GameScriptEvent.AIRotateToTarget);
		}
	}
}