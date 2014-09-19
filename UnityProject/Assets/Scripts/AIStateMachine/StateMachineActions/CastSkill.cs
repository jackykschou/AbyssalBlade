using Assets.Scripts.Constants;
using Assets.Scripts.Utility;
using StateMachine.Action;

namespace Assets.Scripts.AIStateMachine.StateMachineActions{
	[Info (category = "Custom",
	description = "Use AICaster to cast a skill", 
	url = "")]
	public class CastSkill : StateAction
	{
		public override void OnEnter()
		{
		}

		public override void OnUpdate()
		{
            stateMachine.owner.TriggerGameScriptEvent(GameScriptEvent.AICastSkill);
		}
	}
}