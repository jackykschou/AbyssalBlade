using System.Collections;
using Assets.Scripts.Attributes;
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

        protected override void Initialize()
        {
            base.Initialize();
            Destroyed = false;
            Invincible = false;
        }

        [GameScriptEvent(Constants.GameScriptEvent.ObjectTakeFixDamage)]
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

        [GameScriptEvent(Constants.GameScriptEvent.ObjectTakeCurrentPercentageDamage)]
        public void TakeDamageCurrentPercentage(float percentage)
        {
            TakeDamageFixed(HitPoint.Value * percentage);
        }

        [GameScriptEvent(Constants.GameScriptEvent.ObjectTakeMaxPercentageDamage)]
        public void TakeDamageMaxPercentage(float percentage)
        {
            TakeDamageFixed(HitPoint.Max * percentage);
        }

        [GameScriptEvent(Constants.GameScriptEvent.ObjectTakeFixDamagePerSec)]
        public void TakeDamageFixedPerSecond(float amount, int duration)
        {
            StartCoroutine(TakeFixedDamagePerSecondIE(amount, duration));
        }

        [GameScriptEvent(Constants.GameScriptEvent.ObjectTakeCurrentPercentageDamagePerSec)]
        public void TakeDamageCurrentPercentagePerSecond(float percentage, int duration)
        {
            StartCoroutine(TakeFixedDamagePerSecondIE(HitPoint.Value * percentage, duration));
        }

        [GameScriptEvent(Constants.GameScriptEvent.ObjectTakeMaxPercentageDamagePerSec)]
        public void TakeDamageMaxPercentagePerSecond(float percentage, int duration)
        {
            StartCoroutine(TakeFixedDamagePerSecondIE(HitPoint.Max * percentage, duration));
        }

        public IEnumerator TakeFixedDamagePerSecondIE(float amount, int duration)
        {
            while (duration >= 0)
            {
                yield return new WaitForSeconds(1.0f);
                TakeDamageFixed(amount);
                duration -= 1;
            }
        }

        protected override void Deinitialize()
        {
        }
    }
}
