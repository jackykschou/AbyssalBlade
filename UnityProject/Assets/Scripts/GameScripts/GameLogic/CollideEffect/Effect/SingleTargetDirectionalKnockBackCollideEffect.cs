using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.Components;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.CollideEffect.Effect
{
    [AddComponentMenu("CollideEffectTrigger/Effect/SingleTargetDirectionalKnockBackCollideEffect")]
    public class SingleTargetDirectionalKnockBackCollideEffect : CollideTriggerEffect 
    {
        public PositionIndicator Position;
        [Range(0f, 100)]
        public float Radius;
        [Range(0f, 1000f)]
        public float KnockBackSpeed;

        public override void OnCollideTriggerTriggered(GameObject target)
        {
            target.TriggerGameScriptEvent(GameScriptEvent.OnCharacterKnockBacked, Position.Direction, KnockBackSpeed);
        }
    }
}
