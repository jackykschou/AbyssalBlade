using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Attributes;
using Assets.Scripts.GameScripts.GameLogic.Skills.CastableCondition;
using Assets.Scripts.GameScripts.GameLogic.Skills.SkillCasters;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills
{
    [AddComponentMenu("Skill/Skill")]
    public sealed class Skill : GameLogic
    {
        public SkillCaster Caster { get; private set; }

        [Range(0f, 1f)] 
        private float _coolDownPercentage;

        private List<SkillCastableCondition> _castableConditions;

        public void Activate()
        {
            if (_castableConditions.All(c => c.CanCast()))
            {
                Caster.TriggerGameScriptEvent(Constants.GameScriptEvent.SkillCastTriggerSucceed);
            }
            else
            {
                Caster.TriggerGameScriptEvent(Constants.GameScriptEvent.SkillCastTriggerSucceed);
            }
        }

        [GameScriptEvent(Constants.GameScriptEvent.UpdateSkillCooldownPercentage)]
        public void UpdateCoolDownPercentage(float percentage)
        {
            _coolDownPercentage = percentage;
        }

        protected override void Initialize()
        {
            if (transform.parent == null || transform.parent.gameObject.GetComponent<SkillCaster>() == null)
            {
                throw new Exception("A Skill must have a parent that is the caster");
            }

            Caster = transform.parent.gameObject.GetComponent<SkillCaster>();

            base.Initialize();

            _coolDownPercentage = 0f;
            InitializeLists();
        }

        private void InitializeLists()
        {
            _castableConditions = new List<SkillCastableCondition>();
        }

        protected override void Deinitialize()
        {
        }
    }
}
