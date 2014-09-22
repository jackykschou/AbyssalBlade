using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.Components;
using Assets.Scripts.GameScripts.Components.GameValue;
using Assets.Scripts.Utility;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [AddComponentMenu("Skill/SkillEffect/OneTimeCircleAreaDamage")]
    public class OneTimeCircleAreaDamage : SkillEffect
    {
        public GameValue DamageAmount;
        public PositionIndicator Position;
        public float Radius;

        public override void Activate()
        {
            base.Activate();
            ApplyDamages();
            Activated = false;
        }

        public void ApplyDamages()
        {
            foreach (var col in Physics2D.OverlapCircleAll(Position.Position.position, Radius, LayerConstants.LayerMask.Destroyable))
            {
                if (TagConstants.IsEnemy(gameObject.tag, col.gameObject.tag) && !col.gameObject.IsDestroyed())
                {
                    col.gameObject.TriggerGameScriptEvent(GameScriptEvent.OnObjectTakeDamage, DamageAmount.Value);
                }
            }
        }

        [GameScriptEventAttribute(GameScriptEvent.UpdateFacingDirection)]
        public void UpdateDamageAreaPosition(FacingDirection facingDirection)
        {
            Position.UpdatePosition(facingDirection);
        }
    }
}
