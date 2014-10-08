using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.LevelMechanics.Section.SectionObjectives
{
    [AddComponentMenu("LevelMechanics/Section/SectionObjectiveTracker")]
    public class SectionObjectiveTracker : SectionLogic
    {
        public List<SectionObjective> Objectives;
        private const float StartTrackObjectivesDelay = 2.0f;
        private const float TrackObjectivesInterval = 1.0f;

        public override void OnSectionActivated(int sectionId)
        {
            base.OnSectionActivated(sectionId);
            if (sectionId == SectionId)
            {
                InvokeRepeating("TrackObjective", StartTrackObjectivesDelay, TrackObjectivesInterval);
            }
        }

        public void TrackObjective()
        {
            if (Objectives.All(o => o.ObjectiveCompleted()))
            {
                TriggerGameEvent(GameEvent.OnSectionObjectivesCompleted, SectionId);
                CancelInvoke();
            }
        }

        protected override void Initialize()
        {
            base.Initialize();
            Objectives = GetComponents<SectionObjective>().ToList();
        }

        protected override void Deinitialize()
        {
        }
    }
}
