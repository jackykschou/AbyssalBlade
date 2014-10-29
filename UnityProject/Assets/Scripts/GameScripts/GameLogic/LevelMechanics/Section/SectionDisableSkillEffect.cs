using UnityEngine;
using System.Collections.Generic;

using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.LevelMechanics.Section
{
    [AddComponentMenu("LevelMechanics/Section/SectionDisableSkillEfect")]
    public class SectionDisableSkillEffect : SectionLogic
    {
        public List<int> DisabledSkills;

        protected override void Deinitialize()
        {
        }

        public override void OnSectionActivated(int sectionId)
        {
            base.OnSectionActivated(sectionId);
            if (SectionId == sectionId)
            {
                foreach (int skillID in DisabledSkills)
                    TriggerGameEvent(GameEvent.DisableAbility, skillID);
            }
        }

        public override void OnSectionDeactivated(int sectionId)
        {
            base.OnSectionDeactivated(sectionId);
            if (SectionId == sectionId)
            {
                foreach (int skillID in DisabledSkills)
                    TriggerGameEvent(GameEvent.EnableAbility, skillID);
            }
        }

    }
}
