using Assets.Scripts.Constants;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers
{
    [AddComponentMenu("Skill/SkillEffect/TargetEffectApplier/HealthChanger")]
    public class HealthChanger : TargetEffectApplier
    {
        public HealthChangeType HealthChangeType;
        public bool Stackable;
        public HealthModifierNonStackableLabel NonStackableLabel;
        public Components.GameValue.GameValue Amount;
        [Range(0f, 1.0f)]
        public float Percentage;
        [Range(0f, int.MaxValue)]
        public int Duration;
        [Range(0, 1.0f)] 
        public float CriticalChance = 0f;
        [Range(0, 10.0f)]
        public float CriticalDamagePercentage = 2.0f;

        protected override void ApplyEffect(GameObject target)
        {
            ApplyHealthChange(target);
        }

        private void ApplyHealthChange(GameObject target)
        {
            bool crit = MathUtility.RollChance(CriticalChance);
            switch (HealthChangeType)
            {
                case HealthChangeType.Fixed:
                    ApplyFixedChange(target, crit);
                    break;
                case HealthChangeType.CurrentPercentage:
                    ApplyCurrentPercentageChange(target, crit);
                    break;
                case HealthChangeType.MaxPercentage:
                    ApplyMaxPercentageChange(target, crit);
                    break;
                case HealthChangeType.FixedPerSecond:
                    ApplyPerSecondFixedChange(target);
                    break;
                case HealthChangeType.CurrentPercentagePerSecond:
                    ApplyPerSecondCurrentPercentageChange(target);
                    break;
                case HealthChangeType.MaxPercentagePerSecond:
                    ApplyPerSecondMaxPercentageChange(target);
                    break;
            }
        }

        private void ApplyFixedChange(GameObject target, bool isCrit)
        {
            target.TriggerGameScriptEvent(GameScriptEvent.ObjectChangeHealthFix, Amount.Value * GetCriticalMultipler(isCrit), isCrit);
        }

        private void ApplyCurrentPercentageChange(GameObject target, bool isCrit)
        {
            target.TriggerGameScriptEvent(GameScriptEvent.ObjectChangeCurrentPercentageHealth, Percentage * GetCriticalMultipler(isCrit), isCrit);
        }

        private void ApplyMaxPercentageChange(GameObject target, bool isCrit)
        {
            target.TriggerGameScriptEvent(GameScriptEvent.ObjectChangeMaxPercentageHealth, Percentage * GetCriticalMultipler(isCrit), isCrit);
        }

        private void ApplyPerSecondFixedChange(GameObject target)
        {
            target.TriggerGameScriptEvent(GameScriptEvent.ObjectChangeFixHealthPerSec, Amount.Value, Duration, Stackable, NonStackableLabel);
        }

        private void ApplyPerSecondCurrentPercentageChange(GameObject target)
        {
            target.TriggerGameScriptEvent(GameScriptEvent.ObjectChangeCurrentPercentageHealthPerSec, Percentage, Duration, Stackable, NonStackableLabel);
        }

        private void ApplyPerSecondMaxPercentageChange(GameObject target)
        {
            target.TriggerGameScriptEvent(GameScriptEvent.ObjectChangeMaxPercentageHealthPerSec, Percentage, Duration, Stackable, NonStackableLabel);
        }

        private float GetCriticalMultipler(bool isCrit)
        {
            return isCrit ? CriticalDamagePercentage : 1.0f;
        }

        protected override void Deinitialize()
        {
        }
    }
}
