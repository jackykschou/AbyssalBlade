﻿using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.Components.TimeDispatcher;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [AddComponentMenu("Skill/SkillEffect/SetCombo")]
    public class SetCombo : SkillEffect
    {
        public string ComboIntParameterName;

        [Range(0, 10)] 
        public int MaxComboValue;

        public FixTimeDispatcher ComboResetTime;

        private int _currentComboValue;

        protected override void Initialize()
        {
            base.Initialize();
            _currentComboValue = 0;
        }

        public override void Activate()
        {
            base.Activate();
            if (ComboResetTime.CanDispatch())
            {
                _currentComboValue = 0;
            }
            else
            {
                _currentComboValue = (_currentComboValue + 1) % MaxComboValue;
            }
            TriggerCasterGameScriptEvent(GameScriptEvent.SetAnimatorFloatState, ComboIntParameterName, _currentComboValue);
            ComboResetTime.ResetTime();
            Activated = false;
        }
    }
}
