using Assets.Scripts.Constants;
using Assets.Scripts.Utility;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.AILogic
{
    [AddComponentMenu("AILogic/RotateTowardsTarget")]
    public class RotatesTowardTarget : GameLogic
    {
        public Vector3 Target;

        [GameScriptEventAttribute(GameScriptEvent.OnNewTargetDiscovered)]
        public void UpdateTarget(GameObject target)
        {
            Target = target.transform.position;
        }

        public void RotateTowardsTarget()
        {
            FacingDirection newDirection = MathUtility.GetDirection(transform.position, Target).GetFacingDirection();
            if (newDirection != GameView.FacingDirection)
            {
                TriggerGameScriptEvent(GameScriptEvent.UpdateFacingDirection, newDirection);
            }
        }

        protected override void Deinitialize()
        {
        }
    }
}
