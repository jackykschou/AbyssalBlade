using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Constants;
using UnityEngine;
using GameLogicEvent = Assets.Scripts.Constants.GameLogicEvent;
using GameLogicEventAttribute = Assets.Scripts.Attributes.GameLogicEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Skills
{
    public class Skill : GameLogic
    {
        [Range(0f, 1f)] 
        public float CoolDownPercentage;

        protected List<SkillCastableCondition> CastableConditions;

        protected void SkillCastInputReceived(SkillCastInput input)
        {
            if (CastableConditions.All(c => c.CanCast()))
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
            CoolDownPercentage = percentage;
        }

        protected override void Initialize()
        {
            base.Initialize();

            CoolDownPercentage = 0f;
            InitializeLists();
        }

        private void InitializeLists()
        {
            CastableConditions = new List<SkillCastableCondition>();
        }

        protected override void Deinitialize()
        {
        }
    }
}
