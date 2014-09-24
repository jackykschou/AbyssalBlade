using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.Components.GameValue;
using UnityEngine;
using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Destroyable
{
    public class Destroyable : GameLogic
    {
        public bool Invincible;
        public GameValue HitPoint;
        public GameValue DamageReduction;
        public bool Destroyed { get; private set; }

        [Range(0f, float.MaxValue)] 
        [SerializeField]
        private float _delay = 1.5f;

        private Dictionary<DamageNonStackableLabel, int> _currentDamageNonStackableLabelMap;

        protected override void Initialize()
        {
            base.Initialize();
            _currentDamageNonStackableLabelMap = new Dictionary<DamageNonStackableLabel, int>();
            Destroyed = false;
            Invincible = false;
        }

        [Attributes.GameScriptEvent(Constants.GameScriptEvent.ObjectTakeFixDamage)]
        public void TakeDamageFixed(float damage)
        {
            if (Invincible || Destroyed || damage <= 0f)
            {
                return;
            }

            if (DamageReduction >= 1.0f)
            {
                HitPoint -= 1.0f;
            }

            HitPoint -= ((1 - DamageReduction) * damage);
            TriggerGameScriptEvent(Constants.GameScriptEvent.OnObjectTakeDamage);

            if (HitPoint <= 0f)
            {
                Destroyed = true;
                TriggerGameScriptEvent(Constants.GameScriptEvent.OnObjectDestroyed);
                DisableGameObject(_delay);
            }
        }

        [Attributes.GameScriptEvent(Constants.GameScriptEvent.ObjectTakeCurrentPercentageDamage)]
        public void TakeDamageCurrentPercentage(float percentage)
        {
            TakeDamageFixed(HitPoint.Value * percentage);
        }

        [Attributes.GameScriptEvent(Constants.GameScriptEvent.ObjectTakeMaxPercentageDamage)]
        public void TakeDamageMaxPercentage(float percentage)
        {
            TakeDamageFixed(HitPoint.Max * percentage);
        }

        [Attributes.GameScriptEvent(Constants.GameScriptEvent.ObjectTakeFixDamagePerSec)]
        public void TakeDamageFixedPerSecond(float amount, int duration, bool stackable, DamageNonStackableLabel nonStackableLabel)
        {
            if (stackable || _currentDamageNonStackableLabelMap.ContainsKey(nonStackableLabel))
            {
                StartCoroutine(TakeFixedDamagePerSecondStackableIE(amount, duration));
            }
            else
            {
                if (_currentDamageNonStackableLabelMap.ContainsKey(nonStackableLabel))
                {
                    _currentDamageNonStackableLabelMap[nonStackableLabel] = duration;
                }
                else
                {
                    _currentDamageNonStackableLabelMap.Add(nonStackableLabel, duration);
                    StartCoroutine(TakeFixedDamagePerSecondIE(amount, nonStackableLabel));
                }
            }
        }

        [Attributes.GameScriptEvent(Constants.GameScriptEvent.ObjectTakeCurrentPercentageDamagePerSec)]
        public void TakeDamageCurrentPercentagePerSecond(float percentage, int duration, bool stackable, DamageNonStackableLabel nonStackableLabel)
        {
            TakeDamageFixedPerSecond(HitPoint.Value * percentage, duration, stackable, nonStackableLabel);
        }

        [Attributes.GameScriptEvent(Constants.GameScriptEvent.ObjectTakeMaxPercentageDamagePerSec)]
        public void TakeDamageMaxPercentagePerSecond(float percentage, int duration, bool stackable, DamageNonStackableLabel nonStackableLabel)
        {
            TakeDamageFixedPerSecond(HitPoint.Max * percentage, duration, stackable, nonStackableLabel);
        }

        public IEnumerator TakeFixedDamagePerSecondStackableIE(float amount, int duration)
        {
            while (duration >= 0)
            {
                yield return new WaitForSeconds(1.0f);
                TakeDamageFixed(amount);
                duration -= 1;
            }
        }

        public IEnumerator TakeFixedDamagePerSecondIE(float amount, DamageNonStackableLabel nonStackableLabel)
        {
            while (_currentDamageNonStackableLabelMap.ContainsKey(nonStackableLabel) && _currentDamageNonStackableLabelMap[nonStackableLabel] >= 0)
            {
                yield return new WaitForSeconds(1.0f);
                TakeDamageFixed(amount);
                _currentDamageNonStackableLabelMap[nonStackableLabel] =  _currentDamageNonStackableLabelMap[nonStackableLabel] - 1;
            }
            _currentDamageNonStackableLabelMap.Remove(nonStackableLabel);
        }

        protected override void Deinitialize()
        {
        }
    }
}
