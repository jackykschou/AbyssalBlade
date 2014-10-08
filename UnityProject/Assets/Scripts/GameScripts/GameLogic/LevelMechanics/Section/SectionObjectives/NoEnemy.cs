﻿using UnityEngine;
using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.LevelMechanics.Section.SectionObjectives
{
    [AddComponentMenu("LevelMechanics/Section/SectionObjective/NoEnemy")]
    public class NoEnemy : SectionObjective
    {
        private int _enemyCount;

        protected override void Deinitialize()
        {
        }

        public override bool ObjectiveCompleted()
        {
            return _enemyCount == 0;
        }

        protected override void Initialize()
        {
            base.Initialize();
            _enemyCount = 0;
        }

        [GameEventAttribute(GameEvent.OnSectionEnemySpawned)]
        public void OnSectionEnemySpawned(int sectionId, GameObject enemy)
        {
            if (sectionId == SectionId)
            {
                _enemyCount++;
            }
        }

        [GameEventAttribute(GameEvent.OnSectionEnemyDespawned)]
        public void OnSectionEnemyDespawned(int sectionId, GameObject enemy)
        {
            if (sectionId == SectionId)
            {
                _enemyCount--;
            }
        }
    }
}
