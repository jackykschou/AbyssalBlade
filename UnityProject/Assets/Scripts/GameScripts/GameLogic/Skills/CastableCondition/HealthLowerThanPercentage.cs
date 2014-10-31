using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.CastableCondition
{
    [AddComponentMenu("Skill/CastableCondition/HealthLowerThanPercentage")]
    public class HealthLowerThanPercentage : SkillCastableCondition
    {
        [Range(0f, 1.0f)]
        public float LowerThanValuePercentage;
        public Health.Health Health;

        protected override void Deinitialize()
        {
        }

        public override bool CanCast()
        {
            return Health.HitPoint.Percentage <= LowerThanValuePercentage;
        }
    }
}
