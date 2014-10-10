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
            switch (HealthChangeType)
            {
                case HealthChangeType.Fixed:
                    ApplyFixedChange(target);
                    break;
                case HealthChangeType.CurrentPercentage:
                    ApplyCurrentPercentageChange(target);
                    break;
                case HealthChangeType.MaxPercentage:
                    ApplyMaxPercentageChange(target);
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

        private void ApplyFixedChange(GameObject target)
        {
            target.TriggerGameScriptEvent(GameScriptEvent.ObjectChangeHealthFix, Amount.Value * GetCriticalMultipler());
        }

        private void ApplyCurrentPercentageChange(GameObject target)
        {
            target.TriggerGameScriptEvent(GameScriptEvent.ObjectChangeCurrentPercentageHealth, Percentage * GetCriticalMultipler());
        }

        private void ApplyMaxPercentageChange(GameObject target)
        {
            target.TriggerGameScriptEvent(GameScriptEvent.ObjectChangeMaxPercentageHealth, Percentage * GetCriticalMultipler());
        }

        private void ApplyPerSecondFixedChange(GameObject target)
        {
            target.TriggerGameScriptEvent(GameScriptEvent.ObjectChangeFixHealthPerSec, Amount.Value * GetCriticalMultipler(), Duration, Stackable, NonStackableLabel);
        }

        private void ApplyPerSecondCurrentPercentageChange(GameObject target)
        {
            target.TriggerGameScriptEvent(GameScriptEvent.ObjectChangeCurrentPercentageHealthPerSec, Percentage * GetCriticalMultipler(), Duration, Stackable, NonStackableLabel);
        }

        private void ApplyPerSecondMaxPercentageChange(GameObject target)
        {
            target.TriggerGameScriptEvent(GameScriptEvent.ObjectChangeMaxPercentageHealthPerSec, Percentage * GetCriticalMultipler(), Duration, Stackable, NonStackableLabel);
        }

        private float GetCriticalMultipler()
        {
            return MathUtility.RollChance(CriticalChance) ? CriticalDamagePercentage : 1.0f;
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
