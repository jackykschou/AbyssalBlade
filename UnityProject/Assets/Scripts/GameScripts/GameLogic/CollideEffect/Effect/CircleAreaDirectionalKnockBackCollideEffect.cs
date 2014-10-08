using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.Components;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.CollideEffect.Effect
{
    [AddComponentMenu("CollideEffectTrigger/Effect/CircleAreaDirectionalKnockBackCollideEffect")]
    public class CircleAreaDirectionalKnockBackCollideEffect : CollideTriggerEffect
    {
        public PositionIndicator Position;
        [Range(0f, 100)]
        public float Radius;
        [Range(0f, 1000f)]
        public float KnockBackSpeed;
        [Range(0f, 10f)]
        public float Time;

        protected override void Deinitialize()
        {
        }

        public override void OnCollideTriggerTriggered(GameObject target)
        {
            foreach (var col in Physics2D.OverlapCircleAll(transform.position, Radius, LayerConstants.LayerMask.Destroyable))
            {
                col.gameObject.TriggerGameScriptEvent(GameScriptEvent.OnCharacterKnockBacked, Position.Direction, KnockBackSpeed, Time);
            }
        }
    }
}
