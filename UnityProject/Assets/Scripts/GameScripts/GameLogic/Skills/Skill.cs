using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.GameScripts.GameLogic.Skills.CastableCondition;
using UnityEngine;
using GameLogicEvent = Assets.Scripts.Constants.GameLogicEvent;
using GameLogicEventAttribute = Assets.Scripts.Attributes.GameLogicEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Skills
{
    [AddComponentMenu("Skill/Skill")]
    public sealed class Skill : GameLogic
    {
        [Range(0f, 1f)] 
        private float _coolDownPercentage;

        private List<SkillCastableCondition> _castableConditions;

        public void Activate()
        {
            if (_castableConditions.All(c => c.CanCast()))
            {
                TriggerGameLogicEvent(GameLogicEvent.SkillCastTriggerSucceed);
            }
            else
            {
                TriggerGameLogicEvent(GameLogicEvent.SkillCastTriggerFailed);
            }
        }

        [GameLogicEventAttribute(GameLogicEvent.UpdateSkillCooldownPercentage)]
        public void UpdateCoolDownPercentage(float percentage)
        {
            _coolDownPercentage = percentage;
        }

        protected override void Initialize()
        {
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
