using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.Components;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.CollideEffect.Effect
{
    [AddComponentMenu("CollideEffectTrigger/Effect/SingleTargetDivergeKnockBackCollideEffect")]
    public class SingleTargetDivergeKnockBackCollideEffect : CollideTriggerEffect
    {
        [Range(0f, 100)]
        public float Radius;
        [Range(0f, 1000f)]
        public float KnockBackSpeed;

        public override void OnCollideTriggerTriggered(GameObject target)
        {
            target.TriggerGameScriptEvent(GameScriptEvent.OnCharacterKnockBacked, MathUtility.GetDirection(transform.position, target.transform.position), KnockBackSpeed);
        }
    }
}
