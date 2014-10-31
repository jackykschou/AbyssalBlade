﻿using Assets.Scripts.Managers;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;
using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.LevelMechanics.Section
{
    [AddComponentMenu("LevelMechanics/Section/Section")]
    public class Section : SectionLogic
    {
        public override void OnSectionActivated(int sectionId)
        {
            base.OnSectionActivated(SectionId);
            if (sectionId == SectionId)
            {
                LevelManager.Instance.CurrentSectionId = SectionId;
            }
        }

        [GameEventAttribute(GameEvent.OnSectionObjectivesCompleted)]
        public void OnSectionObjectivesCompleted(int sectionId)
        {
            if (sectionId == SectionId)
            {
                TriggerGameEvent(GameEvent.OnSectionDeactivated, SectionId);
            }
        }

        protected override void Initialize()
        {
            base.Initialize();
            TriggerGameScriptEvent(GameScriptEvent.UpdateSectionId, SectionId);
        }

        protected override void Deinitialize()
        {
        }
    }
}
