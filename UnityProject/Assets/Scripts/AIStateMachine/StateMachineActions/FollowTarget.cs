using StateMachine;
using StateMachine.Action;
using Assets.Scripts.GameScripts.GameLogic.AILogic;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.AIStateMachine.StateMachineActions
{
	[Info (category = "Custom",
	description = "Follow target", 
	url = "")]
	public class FollowTarget : StateAction
	{
        [FieldInfo(tooltip = "AI will not move if distance between the target is within distance")]
	    public FloatParameter MinimumDistance;

		public override void OnEnter()
		{
		
		}

		public override void OnUpdate()
		{
            PathFinding pathfinding = stateMachine.owner.GetComponent<PathFinding>();

            if (pathfinding.Target == null || (Vector2.Distance(pathfinding.Target.position, stateMachine.owner.transform.position) <= MinimumDistance))
            {
                return;
            }

            pathfinding.TriggerGameScriptEvent(GameScriptEvent.AIMove, pathfinding.GetMoveDirection());
		}
	}
}