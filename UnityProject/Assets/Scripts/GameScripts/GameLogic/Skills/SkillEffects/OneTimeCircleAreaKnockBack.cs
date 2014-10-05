using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.Components;
using Assets.Scripts.Utility;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [AddComponentMenu("Skill/SkillEffect/OneTimeCircleAreaKnockBack")]
    public class OneTimeCircleAreaKnockBack : SkillEffect 
    {
        public PositionIndicator Position;
        [Range(0f, 100)]
        public float Radius;
        [Range(0f, 1000f)]
        public float KnockBackSpeed;

        protected override void Initialize()
        {
        }

        public override void Activate()
        {
            base.Activate();
            ApplyAreaKnockBack();
            Activated = false;
        }

        public void ApplyAreaKnockBack()
        {
            foreach (var col in Physics2D.OverlapCircleAll(Position.Position.position, Radius, LayerConstants.LayerMask.Destroyable))
            {
                col.gameObject.TriggerGameScriptEvent(GameScriptEvent.OnCharacterKnockBacked, -MathUtility.GetDirection(Position.Position.position, col.gameObject.transform.position), KnockBackSpeed);
            }
        }

        [GameScriptEventAttribute(GameScriptEvent.UpdateFacingDirection)]
        public void UpdateDamageAreaPosition(FacingDirection facingDirection)
        {
            Position.UpdatePosition(facingDirection);
        }
    }
}
