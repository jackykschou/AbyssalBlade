using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.AILogic;
using Assets.Scripts.Utility;
using StateMachine;
using StateMachine.Action;
using UnityEngine;

namespace Assets.Scripts.AIStateMachine.StateMachineActions{
	[Info (category = "Custom",
	description = "Random Patrol around", 
	url = "")]
    public class Patrol : StateAction
	{
        [FieldInfo(tooltip = "Maxium time allowed of a patrol path before renewing a new path")]
        public FloatParameter MaxiumSinglePathTime;
        [FieldInfo(tooltip = "Radius within which the patrol point is allowed to chose from")]
        public FloatParameter PatrolPointSelectionRadius;

	    private float _currentPathPatroltime;
	    private GameObject _patrolPoint;

		public override void OnEnter()
		{
		    _currentPathPatroltime = 0f;
            if (_patrolPoint == null)
		    {
		        _patrolPoint = new GameObject();
		    }
		}

		public override void OnUpdate()
		{
		}

	    public override void OnFixedUpdate()
	    {
            _currentPathPatroltime += Time.fixedDeltaTime;

            if (stateMachine.owner.HitPointAtZero() || stateMachine.owner.IsInterrupted())
            {
                return;
            }

            PathFinding pathfinding = stateMachine.owner.GetComponent<PathFinding>();

            if (_currentPathPatroltime >= MaxiumSinglePathTime || !pathfinding.CurrentPathReachable || _patrolPoint || (Vector2.Distance(_patrolPoint.transform.position, stateMachine.owner.transform.position) <= 0.5f))
            {
                Vector3 newPatrolPointPosition = new Vector3(stateMachine.owner.transform.position.x + Random.Range(-PatrolPointSelectionRadius, PatrolPointSelectionRadius), 
                    stateMachine.owner.transform.position.y + Random.Range(-PatrolPointSelectionRadius, PatrolPointSelectionRadius),
                    stateMachine.owner.transform.position.z);
                _patrolPoint.transform.position = newPatrolPointPosition;
                pathfinding.UpdateTarget(_patrolPoint);
                _currentPathPatroltime = 0f;
            }

            pathfinding.TrySearchPath();

	        Vector2 moveDirection = pathfinding.GetMoveDirection();

            if ((moveDirection == Vector2.zero) || !pathfinding.CurrentPathReachable)
            {
                return;
            }

            pathfinding.TriggerGameScriptEvent(GameScriptEvent.CharacterMove, moveDirection);
	    }
	}
}