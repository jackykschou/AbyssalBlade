using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.Components;
using Assets.Scripts.GameScripts.Components.GameValue;
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

        public void ApplyDamages()
        {
            foreach (var hit in Physics2D.CircleCastAll(Position.Position.position, Radius, Vector2.zero, 0f, LayerConstants.LayerMask.Destroyable))
            {
                if (TagConstants.IsEnemy(gameObject.tag, hit.collider.gameObject.tag))
                {
                    if (hit.collider.gameObject.GetComponent<GameScript>() != null)
                    {
                        hit.collider.gameObject.GetComponent<GameScript>().TriggerGameScriptEvent(GameScriptEvent.OnObjectTakeDamage, DamageAmount);
                    }
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
