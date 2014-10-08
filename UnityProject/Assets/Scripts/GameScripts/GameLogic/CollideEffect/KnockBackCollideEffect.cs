using Assets.Scripts.GameScripts.Components;
using Assets.Scripts.Utility;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.CollideEffect
{
    public class AreaKnockBackCollideEffect : GameLogic 
    {
        public PositionIndicator Position;
        [Range(0f, 1000f)]
        public float KnockBackSpeed;

        protected override void Deinitialize()
        {
        }

        [GameScriptEventAttribute(GameScriptEvent.OnObjectCollideWithCollideTrigger)]
        public void ApplyKnockBack(GameObject target)
        {
            TriggerGameScriptEvent(GameScriptEvent.OnCharacterKnockBacked, -MathUtility.GetDirection(Position.Position.position, target.transform.position), KnockBackSpeed);
        }
    }
}
