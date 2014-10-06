using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.Components;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.CollideEffect.Effect
{
    [AddComponentMenu("CollideEffectTrigger/Effect/CircleAreaDivergeKnockBackCollideEffect")]
    public class CircleAreaDivergeKnockBackCollideEffect : CollideTriggerEffect 
    {
        [Range(0f, 1000f)]
        public float KnockBackSpeed;
        [Range(0f, 100f)]
        public float Radius;

        protected override void Deinitialize()
        {
        }

        public override void OnCollideTriggerTriggered(GameObject target)
        {
            foreach (var col in Physics2D.OverlapCircleAll(transform.position, Radius, LayerConstants.LayerMask.Destroyable))
            {
                col.gameObject.TriggerGameScriptEvent(GameScriptEvent.OnCharacterKnockBacked, MathUtility.GetDirection(transform.position, col.gameObject.transform.position), KnockBackSpeed);
            }
        }
    }
}
