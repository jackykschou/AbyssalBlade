using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.Components;
using Assets.Scripts.Utility;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [AddComponentMenu("Skill/SkillEffect/OneTimeCircleAreaInterrupt")]
    public class OneTimeCircleAreaInterrupt : SkillEffect
    {
        public PositionIndicator Position;
        public float Radius;

        public override void Activate()
        {
            base.Activate();
            ApplyCircleAreaInterrupt();
            Activated = false;
        }

        public void ApplyCircleAreaInterrupt()
        {
            foreach (var col in Physics2D.OverlapCircleAll(Position.Position.position, Radius, LayerConstants.LayerMask.Destroyable))
            {
                col.gameObject.TriggerGameScriptEvent(GameScriptEvent.InterruptCharacter);
            }
        }

        [GameScriptEventAttribute(GameScriptEvent.UpdateFacingDirection)]
        public void UpdateDamageAreaPosition(FacingDirection facingDirection)
        {
            Position.UpdatePosition(facingDirection);
        }
    }
}
