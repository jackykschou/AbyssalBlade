using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.Components;
using Assets.Scripts.GameScripts.GameLogic.Health;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [RequireComponent(typeof(HealthChanger))]
    [AddComponentMenu("Skill/SkillEffect/OneTimeCircleAreaHealthChange")]
    public class OneTimeCircleAreaHealthChange : SkillEffect
    {
        public HealthChanger HealthChanger;
        public PositionIndicator Position;
        public float Radius;

        protected override void Initialize()
        {
            base.Initialize();
            if (HealthChanger == null)
            {
                HealthChanger = GetComponent<HealthChanger>();
            }
        }

        public override void Activate()
        {
            base.Activate();
            ApplyAreaHealthChange();
            Activated = false;
        }

        public void ApplyAreaHealthChange()
        {
            foreach (var col in Physics2D.OverlapCircleAll(Position.Position.position, Radius, LayerConstants.LayerMask.Destroyable))
            {
                HealthChanger.ApplyHealthChange(col.gameObject);
            }
        }

        [GameScriptEventAttribute(GameScriptEvent.UpdateFacingDirection)]
        public void UpdateDamageAreaPosition(FacingDirection facingDirection)
        {
            Position.UpdatePosition(facingDirection);
        }
    }
}
