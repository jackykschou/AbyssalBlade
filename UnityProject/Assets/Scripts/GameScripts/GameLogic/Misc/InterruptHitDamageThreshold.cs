using Assets.Scripts.GameScripts.Components.TimeDispatcher;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Misc
{
    public class InterruptHitDamageThreshold : GameLogic 
    {
        [Range(0f, float.MaxValue)]
        public float HitDamageThreshold;

        public FixTimeDispatcher HitDamageThresholdResetTime;

        private float _accumulatedHitDamage;

        [GameScriptEventAttribute(GameScriptEvent.OnObjectTakeDamage)]
        public void UpdateDamageThreshold(float damage)
        {
            if (HitDamageThresholdResetTime.CanDispatch())
            {
                if (damage >= HitDamageThreshold)
                {
                    TriggerGameScriptEvent(GameScriptEvent.InterruptCharacter);
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
                    TriggerGameScriptEvent(GameScriptEvent.InterruptCharacter);
                }
            }
            HitDamageThresholdResetTime.ResetTime();
        }

        [GameScriptEventAttribute(GameScriptEvent.OnCharacterInterrupted)]
        public void ResetAccumulatedHitDamage()
        {
            _accumulatedHitDamage = 0f;
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
