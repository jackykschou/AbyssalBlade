using Assets.Scripts.Constants;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.CollideEffect.Effect
{
    [AddComponentMenu("CollideEffectTrigger/Effect/CircleAreaInterruptCollideEffect")]
    public class CircleAreaInterruptCollideEffect : CollideTriggerEffect 
    {
        [Range(0f, 100)]
        public float Radius;

        public override void OnCollideTriggerTriggered(GameObject target)
        {
            foreach (var col in Physics2D.OverlapCircleAll(transform.position, Radius, LayerConstants.LayerMask.Destroyable))
            {
                col.gameObject.TriggerGameScriptEvent(GameScriptEvent.InterruptCharacter);
            }
        }
    }
}
