using System.Collections.Generic;
using Assets.Scripts.Constants;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.Components.DamageApplier
{
    public class DamageApplier : GameLogic.GameLogic
    {
        public bool OneTimeOnlyPerTarget;
        public DamageApplyType DamageType;
        public bool Stackable;
        public DamageNonStackableLabel NonStackableLabel;
        public GameValue.GameValue Amount;
        [Range(0f, 1.0f)]
        public float Percentage;
        [Range(0f, int.MaxValue)]
        public int Duration;

        private List<GameObject> _damagedCache; 

        public bool ApplyDamage(GameObject target)
        {
            if (TagConstants.IsEnemy(gameObject.tag, target.tag) && !target.IsDestroyed() &&
                (!_damagedCache.Contains(target) || !OneTimeOnlyPerTarget))
            {
                ApplyDamageHelper(target);
                return true;
            }
            return false;
        }

        private void ApplyDamageHelper(GameObject target)
        {
            _damagedCache.Add(target);
            switch (DamageType)
            {
                case DamageApplyType.Fixed:
                    ApplyFixedDamage(target);
                    break;
                case DamageApplyType.CurrentPercentage:
                    ApplyCurrentPercentageDamage(target);
                    break;
                case DamageApplyType.MaxPercentage:
                    ApplyMaxPercentageDamage(target);
                    break;
                case DamageApplyType.FixedPerSecond:
                    ApplyPerSecondFixedDamage(target);
                    break;
                case DamageApplyType.CurrentPercentagePerSecond:
                    ApplyPerSecondCurrentPercentageDamage(target);
                    break;
                case DamageApplyType.MaxPercentagePerSecond:
                    ApplyPerSecondMaxPercentageDamage(target);
                    break;
            }
        }

        private void ApplyFixedDamage(GameObject target)
        {
            target.TriggerGameScriptEvent(GameScriptEvent.ObjectTakeFixDamage, Amount.Value);
        }

        private void ApplyCurrentPercentageDamage(GameObject target)
        {
            target.TriggerGameScriptEvent(GameScriptEvent.ObjectTakeCurrentPercentageDamage, Percentage);
        }

        private void ApplyMaxPercentageDamage(GameObject target)
        {
            target.TriggerGameScriptEvent(GameScriptEvent.ObjectTakeMaxPercentageDamage, Percentage);
        }

        private void ApplyPerSecondFixedDamage(GameObject target)
        {
            target.TriggerGameScriptEvent(GameScriptEvent.ObjectTakeFixDamagePerSec, Amount.Value, Duration, Stackable, NonStackableLabel);
        }

        private void ApplyPerSecondCurrentPercentageDamage(GameObject target)
        {
            target.TriggerGameScriptEvent(GameScriptEvent.ObjectTakeCurrentPercentageDamagePerSec, Percentage, Duration, Stackable, NonStackableLabel);
        }

        private void ApplyPerSecondMaxPercentageDamage(GameObject target)
        {
            target.TriggerGameScriptEvent(GameScriptEvent.ObjectTakeMaxPercentageDamagePerSec, Percentage, Duration, Stackable, NonStackableLabel);
        }

        protected override void Initialize()
        {
            base.Initialize();
            _damagedCache = new List<GameObject>();
        }

        protected override void Deinitialize()
        {
        }
    }
}
