using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.Components.GameValue;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Health
{
    [AddComponentMenu("HealthLogic/Health")]
    public class Health : GameLogic
    {
        private const float VariantPercentage = 0.05f;

        public bool Invincible;
        public GameValue HitPoint;
        [Range(0f, float.MaxValue)]
        public float HealingEmphasizePercentage;
        [Range(0f, float.MaxValue)]
        public float DamageEmphasizePercentage;
        public bool HitPointAtZero { get; private set; }

        private Dictionary<HealthModifierNonStackableLabel, int> _currentHealthModifierNonStackableLabelMap;

        protected override void Initialize()
        {
            base.Initialize();
            _currentHealthModifierNonStackableLabelMap = new Dictionary<HealthModifierNonStackableLabel, int>();
            HitPointAtZero = false;
            Invincible = false;
        }

        [Attributes.GameScriptEvent(GameScriptEvent.ObjectChangeHealthFix)]
        public virtual void ChangeHealthFixed(float amount)
        {
            if (Invincible || HitPointAtZero || Mathf.Approximately(0f, amount))
            {
                return;
            }

            float actualAmount = amount + Random.Range(-amount * VariantPercentage, amount * VariantPercentage);

            if (actualAmount <= 0f)
            {
                actualAmount += actualAmount * DamageEmphasizePercentage;
                TriggerGameScriptEvent(GameScriptEvent.OnObjectTakeDamage, actualAmount);
            }
            else
            {
                actualAmount += actualAmount * HealingEmphasizePercentage;
                TriggerGameScriptEvent(GameScriptEvent.OnObjectTakeHeal, actualAmount);
            }

            HitPoint.Value += actualAmount;

            if (HitPoint <= 0f)
            {
                HitPointAtZero = true;
                HitPoint.Value = 0f;
                TriggerGameScriptEvent(GameScriptEvent.OnOjectHasNoHitPoint);
            }
        }

    
        [Attributes.GameScriptEvent(GameScriptEvent.ObjectChangeCurrentPercentageHealth)]
        public void ChangeHealthCurrentPercentage(float percentage)
        {
            ChangeHealthFixed(HitPoint.Value * percentage);
        }

        [Attributes.GameScriptEvent(GameScriptEvent.ObjectChangeMaxPercentageHealth)]
        public void ChangeHealthMaxPercentage(float percentage)
        {
            ChangeHealthFixed(HitPoint.Max * percentage);
        }

        [Attributes.GameScriptEvent(GameScriptEvent.ObjectChangeFixHealthPerSec)]
        public void ChangeHealthFixedPerSecond(float amount, int duration, bool stackable, HealthModifierNonStackableLabel nonStackableLabel)
        {
            if (stackable || _currentHealthModifierNonStackableLabelMap.ContainsKey(nonStackableLabel))
            {
                StartCoroutine(ChangeHealthPerSecondStackableIE(amount, duration));
            }
            else
            {
                if (_currentHealthModifierNonStackableLabelMap.ContainsKey(nonStackableLabel))
                {
                    _currentHealthModifierNonStackableLabelMap[nonStackableLabel] = duration;
                }
                else
                {
                    _currentHealthModifierNonStackableLabelMap.Add(nonStackableLabel, duration);
                    StartCoroutine(ChangeHealthPerSecondIE(amount, nonStackableLabel));
                }
            }
        }

        [Attributes.GameScriptEvent(GameScriptEvent.ObjectChangeCurrentPercentageHealthPerSec)]
        public void ChangeHealthCurrentPercentagePerSecond(float percentage, int duration, bool stackable, HealthModifierNonStackableLabel nonStackableLabel)
        {
            ChangeHealthFixedPerSecond(HitPoint.Value * percentage, duration, stackable, nonStackableLabel);
        }

        [Attributes.GameScriptEvent(GameScriptEvent.ObjectChangeMaxPercentageHealthPerSec)]
        public void ChangeHealthMaxPercentagePerSecond(float percentage, int duration, bool stackable, HealthModifierNonStackableLabel nonStackableLabel)
        {
            ChangeHealthFixedPerSecond(HitPoint.Max * percentage, duration, stackable, nonStackableLabel);
        }

        public IEnumerator ChangeHealthPerSecondStackableIE(float amount, int duration)
        {
            while (duration >= 0)
            {
                yield return new WaitForSeconds(1.0f);
                ChangeHealthFixed(amount);
                duration -= 1;
            }
        }

        public IEnumerator ChangeHealthPerSecondIE(float amount, HealthModifierNonStackableLabel nonStackableLabel)
        {
            while (_currentHealthModifierNonStackableLabelMap.ContainsKey(nonStackableLabel) && _currentHealthModifierNonStackableLabelMap[nonStackableLabel] >= 0)
            {
                yield return new WaitForSeconds(1.0f);
                ChangeHealthFixed(amount);
                _currentHealthModifierNonStackableLabelMap[nonStackableLabel] =  _currentHealthModifierNonStackableLabelMap[nonStackableLabel] - 1;
            }
            _currentHealthModifierNonStackableLabelMap.Remove(nonStackableLabel);
        }

        protected override void Deinitialize()
        {
        }
    }
}
