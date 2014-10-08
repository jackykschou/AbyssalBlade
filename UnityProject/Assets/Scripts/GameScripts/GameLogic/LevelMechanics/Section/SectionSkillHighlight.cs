using Assets.Scripts.Constants;
using UnityEngine;
using System.Collections.Generic;

using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.LevelMechanics.Section
{
    [AddComponentMenu("LevelMechanics/Section/SectionSkillHighlight")]
    public class SectionSkillHighlight : SectionLogic
    {
        public int SkillToHighlight;
        [Range(0.0f,5.0f)]
        public float duration;

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void Deinitialize()
        {
        }

        public override void OnSectionActivated(int sectionId)
        {
            base.OnSectionActivated(sectionId);
            if (SectionId == sectionId)
            {
                TriggerGameEvent(GameEvent.EnableHighlightSkill, SkillToHighlight, duration);
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
