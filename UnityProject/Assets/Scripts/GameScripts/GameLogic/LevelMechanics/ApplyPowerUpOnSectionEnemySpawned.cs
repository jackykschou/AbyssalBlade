using System.Collections.Generic;
using Assets.Scripts.Constants;
using Assets.Scripts.Managers;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.LevelMechanics
{
    [AddComponentMenu("LevelMechanics/ApplyPowerUpOnSectionEnemySpawned")]
    public class ApplyPowerUpOnSectionEnemySpawned : GameLogic
    {
        public List<Prefab> PowerUpPrefabs;

        public int MaxApplierTime;
        private int _appliedCounter;

        [Attributes.GameScriptEvent(GameScriptEvent.OnSectionEnemySpawned)]
        public void OnSectionEnemySpawned(GameObject enemy)
        {
            if (_appliedCounter > MaxApplierTime)
            {
                return;
            }
            _appliedCounter++;

            Prefab powerupPrefab = PowerUpPrefabs[Random.Range(0, PowerUpPrefabs.Count)];
            PrefabManager.Instance.SpawnPrefab(powerupPrefab, o => o.TriggerGameScriptEvent(GameScriptEvent.ApplyPowerUp, enemy, powerupPrefab));
        }

        [Attributes.GameEvent(GameEvent.SurvivalDifficultyIncreased)]
        public void UpdateMaxApplierTime(int difficulty)
        {
            MaxApplierTime = 1 + (int)(difficulty / 2.0f);
        }

        protected override void Initialize()
        {
            base.Initialize();
            _appliedCounter = 0;
        }

        protected override void Deinitialize()
        {

        }
    }
}
