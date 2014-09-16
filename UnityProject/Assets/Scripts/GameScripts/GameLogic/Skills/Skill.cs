using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Attributes;
using Assets.Scripts.GameScripts.GameLogic.Skills.CastableCondition;
using Assets.Scripts.GameScripts.GameLogic.Skills.SkillCasters;
using UnityEngine;

using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Skills
{
    [AddComponentMenu("Skill/Skill")]
    public sealed class Skill : GameLogic
    {
        public SkillCaster Caster { get; private set; }
        public bool IsActivate;
        public bool OnceAtATime;

        [Range(0f, 1f)] 
        private float _coolDownPercentage;

        private List<SkillCastableCondition> _castableConditions;

        public bool CanActivate()
        {
            return _castableConditions.All(c => c.CanCast()) && (!OnceAtATime || !IsActivate);
        }

        public void Activate()
        {
            if (CanActivate())
            {
                IsActivate = true;
                Caster.TriggerGameScriptEvent(Constants.GameScriptEvent.SkillCastTriggerSucceed, this);
            }
            else
            {
                Caster.TriggerGameScriptEvent(Constants.GameScriptEvent.SkillCastTriggerFailed, this);
            }
        }

        [GameScriptEventAttribute(GameScriptEvent.UpdateSkillCooldownPercentage)]
        public void UpdateCoolDownPercentage(Skill skill, float percentage)
        {
            _coolDownPercentage = percentage;
        }

        [GameScriptEventAttribute(GameScriptEvent.SkillEnded)]
        public void SkillEnded(Skill skill)
        {
            if (skill == this)
            {
                IsActivate = false;
            }
        }

        protected override void Initialize()
        {
            base.Initialize();
            if (transform.parent == null || transform.parent.gameObject.GetComponent<SkillCaster>() == null)
            {
                throw new Exception("A Skill must have a parent that is the caster");
            }
            IsActivate = false;
            Caster = transform.parent.gameObject.GetComponent<SkillCaster>();
            _coolDownPercentage = 0f;
            InitializeLists();
            gameObject.tag = Caster.gameObject.tag;
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
