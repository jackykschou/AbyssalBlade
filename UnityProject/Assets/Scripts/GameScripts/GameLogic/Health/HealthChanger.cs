using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Constants;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Health
{
    [AddComponentMenu("HealthLogic/HealthChanger")]
    public class HealthChanger : GameLogic
    {
        public List<string> TargetTags = new List<string>(); 
        public bool OneTimeOnlyPerTarget;
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

        private List<GameObject> _changedCache;

        public bool ApplyHealthChange(GameObject target)
        {
            if (TargetTagMatch(target.tag) && !target.HitPointAtZero() && (!_changedCache.Contains(target) || !OneTimeOnlyPerTarget))
            {
                ApplyHealthChangeHelper(target);
                return true;
            }
            return false;
        }

        private bool TargetTagMatch(string targetTag)
        {
            return TargetTags.Any(t => t == targetTag);
        }

        private void ApplyHealthChangeHelper(GameObject target)
        {
            _changedCache.Add(target);
            bool crit = MathUtility.RollChance(CriticalChance);
            if (crit)
                target.GetComponent<DamageDisplayer>().TriggerGameScriptEvent(GameScriptEvent.OnObjectWasCrit);
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
                    ApplyPerSecondFixedChange(target, crit);
                    break;
                case HealthChangeType.CurrentPercentagePerSecond:
                    ApplyPerSecondCurrentPercentageChange(target, crit);
                    break;
                case HealthChangeType.MaxPercentagePerSecond:
                    ApplyPerSecondMaxPercentageChange(target, crit);
                    break;
            }
        }

        private void ApplyFixedChange(GameObject target, bool isCrit)
        {
            target.TriggerGameScriptEvent(GameScriptEvent.ObjectChangeHealthFix, Amount.Value * GetCriticalMultipler(isCrit));
        }

        private void ApplyCurrentPercentageChange(GameObject target, bool isCrit)
        {
            target.TriggerGameScriptEvent(GameScriptEvent.ObjectChangeCurrentPercentageHealth, Percentage * GetCriticalMultipler(isCrit));
        }

        private void ApplyMaxPercentageChange(GameObject target, bool isCrit)
        {
            target.TriggerGameScriptEvent(GameScriptEvent.ObjectChangeMaxPercentageHealth, Percentage * GetCriticalMultipler(isCrit));
        }

        private void ApplyPerSecondFixedChange(GameObject target, bool isCrit)
        {
            target.TriggerGameScriptEvent(GameScriptEvent.ObjectChangeFixHealthPerSec, Amount.Value * GetCriticalMultipler(isCrit), Duration, Stackable, NonStackableLabel);
        }

        private void ApplyPerSecondCurrentPercentageChange(GameObject target, bool isCrit)
        {
            target.TriggerGameScriptEvent(GameScriptEvent.ObjectChangeCurrentPercentageHealthPerSec, Percentage * GetCriticalMultipler(isCrit), Duration, Stackable, NonStackableLabel);
        }

        private void ApplyPerSecondMaxPercentageChange(GameObject target, bool isCrit)
        {
            target.TriggerGameScriptEvent(GameScriptEvent.ObjectChangeMaxPercentageHealthPerSec, Percentage * GetCriticalMultipler(isCrit), Duration, Stackable, NonStackableLabel);
        }

        private float GetCriticalMultipler(bool isCrit)
        {
            return isCrit ? CriticalDamagePercentage : 1.0f;
        }

        protected override void Initialize()
        {
            base.Initialize();
            _changedCache = new List<GameObject>();
        }

        protected override void Deinitialize()
        {
        }
    }
}
