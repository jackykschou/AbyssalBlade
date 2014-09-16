using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.Components;
using Assets.Scripts.GameScripts.Components.GameValue;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [AddComponentMenu("Skill/SkillEffect/OneTimeCircleAreaDamage")]
    [RequireComponent(typeof(CircleCollider2D))]
    public class OneTimeCircleAreaDamage : SkillEffect
    {
        public GameValue DamageAmount;
        public PositionIndicator Position;
        public CircleCollider2D Area;

        protected override void Initialize()
        {
            base.Initialize();
            if (Area == null)
            {
                Area = GetComponent<CircleCollider2D>();
            }
            Area.isTrigger = true;
            Area.enabled = false;
        }

        [GameScriptEventAttribute(GameScriptEvent.SkillCastTriggerSucceed)]
        public void ApplyDamages(Skill skill)
        {
            foreach (var hit in Physics2D.CircleCastAll(Area.center, Area.radius, Vector2.zero, 0.1f, LayerConstants.LayerMask.Destroyable))
            {
                if (TagConstants.IsEnemy(gameObject.tag, hit.collider.gameObject.tag))
                {
                    if (hit.collider.gameObject.GetComponent<GameScript>() != null)
                    {
                        hit.collider.gameObject.GetComponent<GameScript>().TriggerGameScriptEvent(GameScriptEvent.OnObjectTakeDamage, DamageAmount);
                    }
                }
            }
            TriggerCasterGameScriptEvent(GameScriptEvent.SkillEnded, SKill);    
        }

        [GameScriptEventAttribute(GameScriptEvent.UpdateFacingDirection)]
        public void UpdateDamageAreaPosition(FacingDirection facingDirection)
        {
            Position.UpdatePosition(facingDirection);
        }
    }
}
