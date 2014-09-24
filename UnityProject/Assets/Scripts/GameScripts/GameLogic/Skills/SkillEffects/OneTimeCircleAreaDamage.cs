using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.Components;
using Assets.Scripts.GameScripts.Components.DamageApplier;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [RequireComponent(typeof(DamageApplier))]
    [AddComponentMenu("Skill/SkillEffect/OneTimeCircleAreaDamage")]
    public class OneTimeCircleAreaDamage : SkillEffect
    {
        public DamageApplier DamagerApplier;
        public PositionIndicator Position;
        public float Radius;

        protected override void Initialize()
        {
            base.Initialize();
            if (DamagerApplier == null)
            {
                DamagerApplier = GetComponent<DamageApplier>();
            }
        }

        public override void Activate()
        {
            base.Activate();
            ApplyAreaDamages();
            Activated = false;
        }

        public void ApplyAreaDamages()
        {
            foreach (var col in Physics2D.OverlapCircleAll(Position.Position.position, Radius, LayerConstants.LayerMask.Destroyable))
            {
                DamagerApplier.ApplyDamage(col.gameObject);
            }
        }

        [GameScriptEventAttribute(GameScriptEvent.UpdateFacingDirection)]
        public void UpdateDamageAreaPosition(FacingDirection facingDirection)
        {
            Position.UpdatePosition(facingDirection);
        }
    }
}
