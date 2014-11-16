﻿using Assets.Scripts.Attributes;
using Assets.Scripts.GameScripts.GameLogic.GameValue;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers
{
    [RequireComponent(typeof(GameValueChanger))]
    [AddComponentMenu("TargetEffectApplier/HealthChanger")]
    public class HealthChanger : TargetEffectApplier
    {
        public GameValueChanger GameValueChanger;

        protected override void ApplyEffect(GameObject target)
        {
            ApplyHealthChange(target);
        }

        private void ApplyHealthChange(GameObject target)
        {
            target.TriggerGameScriptEvent(Constants.GameScriptEvent.ObjectChangeHealth, GameValueChanger);
        }

        protected override void Deinitialize()
        {
        }

        [GameScriptEvent(Constants.GameScriptEvent.ChangeDamageCriticalChance)]
        public void ChangeDamageCriticalChance(float changeAmount)
        {
            if (GameValueChanger._amount <= 0f)
            {
                GameValueChanger.CriticalChance += changeAmount;
            }
        }

        [GameScriptEvent(Constants.GameScriptEvent.ChangeHealthChangerRawAmountToInitialPercentage)]
        public void ChangeHealthChangerRawAmountToOriginalPercentage(float percentage)
        {
            GameValueChanger.RawAmount = GameValueChanger.InitialAmount * percentage;
        }
    }
}
