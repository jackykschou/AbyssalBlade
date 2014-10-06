using System.Collections;
using Assets.Scripts.GameScripts.Components.TimeDispatcher;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Misc
{
    public class CharacterOnHitInterrupt : GameLogic
    {
        public bool Interrupted { get; private set; }

        [Range(0f, float.MaxValue)]
        public float HitDamageThreshold;
        [Range(0f, float.MaxValue)]
        public float InterruptionDuration;

        public FixTimeDispatcher HitDamageThresholdResetTime;
        public FixTimeDispatcher InterruptCoolDown;

        private float _accumulatedHitDamage;

        [GameScriptEventAttribute(GameScriptEvent.InterruptCharacter)]
        public void InterruptCharacter()
        {
            if (!InterruptCoolDown.CanDispatch())
            {
                return;
            }
            _accumulatedHitDamage = 0f;
            InterruptCoolDown.Dispatch();
            TriggerGameScriptEvent(GameScriptEvent.OnCharacterInterrupted);
            StartCoroutine(CountDownInterruption());
        }

        IEnumerator CountDownInterruption()
        {
            Interrupted = true;
            yield return new WaitForSeconds(InterruptionDuration);
            Interrupted = false;
        }

        [GameScriptEventAttribute(GameScriptEvent.OnObjectTakeDamage)]
        public void UpdateDamageThreshold(float damage)
        {
            if (HitDamageThresholdResetTime.CanDispatch())
            {
                if (damage >= HitDamageThreshold)
                {
                    InterruptCharacter();
                }
                else
                {
                    _accumulatedHitDamage += damage;
                }
            }
            else
            {
                _accumulatedHitDamage += damage;
                if (_accumulatedHitDamage >= HitDamageThreshold)
                {
                    InterruptCharacter();
                }
            }
            HitDamageThresholdResetTime.ResetTime();
        }

        protected override void Initialize()
        {
            base.Initialize();
            _accumulatedHitDamage = 0f;
        }

        protected override void Deinitialize()
        {
        }
    }
}
