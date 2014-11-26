using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Managers;
using Assets.Scripts.Utility;
using UnityEngine;
using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.LevelMechanics.Section.SectionObjectives
{
    [AddComponentMenu("LevelMechanics/Section/SectionObjectiveTracker")]
    public class SectionObjectiveTracker : SectionLogic
    {
        public List<SectionObjective> Objectives;
        private const float TrackObjectivesInterval = 1.0f;

        private bool _startTracking;
        private float _trackTimer;

        [GameEventAttribute(GameEvent.OnLevelEnded)]
        public void OnLevelEnded()
        {
            TriggerGameEvent(GameEvent.OnSectionObjectivesCompleted, SectionId);
        }

        public override void OnSectionActivated(int sectionId)
        {
            base.OnSectionActivated(sectionId);
            if (sectionId == SectionId)
            {
                _startTracking = true;
            }
        }

        public void TrackObjective()
        {
            Debug.Log("Tracking Objective......");
            if (GameManager.Instance.PlayerMainCharacter.HitPointAtZero())
            {
                _startTracking = false;
                return;
            }
            if (Objectives.All(o => o.ObjectiveCompleted()) && !GameScriptEventManager.Destroyed)
            {
                Debug.Log("Objective Completed");
                _startTracking = false;
                _trackTimer = 0f;
                TriggerGameEvent(GameEvent.OnSectionObjectivesCompleted, SectionId);
            }
        }

        protected override void Update()
        {
            base.Update();
            if (_startTracking && _trackTimer >= TrackObjectivesInterval)
            {
                _trackTimer = 0f;
                TrackObjective();
            }
            else if (_startTracking)
            {
                _trackTimer += Time.deltaTime;
            }
        }

        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            Objectives = GetComponents<SectionObjective>().ToList();
        }

        protected override void Initialize()
        {
            base.Initialize();
            _startTracking = false;
        }

        protected override void Deinitialize()
        {
        }
    }
}
