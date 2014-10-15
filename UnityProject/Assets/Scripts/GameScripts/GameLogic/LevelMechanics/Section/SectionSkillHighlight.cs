using UnityEngine;
using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.LevelMechanics.Section
{
    [AddComponentMenu("LevelMechanics/Section/SectionSkillHighlight")]
    public class SectionSkillHighlight : SectionLogic
    {
        public int SkillToHighlight;
        private const float Duration = .5f;

        protected override void Deinitialize()
        {
        }

        public override void OnSectionActivated(int sectionId)
        {
            base.OnSectionActivated(sectionId);
            if (SectionId == sectionId)
            {
                TriggerGameEvent(GameEvent.EnableHighlightSkill, SkillToHighlight, Duration);
            }
        }

        public override void OnSectionDeactivated(int sectionId)
        {
            base.OnSectionDeactivated(sectionId);
            if (SectionId == sectionId)
            {
                TriggerGameEvent(GameEvent.DisableHighlightSkill, SkillToHighlight);
            }
        }

    }
}
