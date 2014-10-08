using System.Collections.Generic;
using Assets.Scripts.GameScripts.GameLogic.LevelMechanics.Section;
using Assets.Scripts.GameScripts.GameLogic.Spawner;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.LevelMechanics
{
    [RequireComponent(typeof(Health.Health))]
    [AddComponentMenu("LevelMechanics/Section/DeactivateEnemySpawnPointsOnNoHitPoint")]
    public class DeactivateEnemySpawnPointsOnNoHitPoint : SectionLogic
    {
        public List<SectionEnemySpawnPoint> SpawnPoints;

        [GameScriptEventAttribute(GameScriptEvent.OnObjectHasNoHitPoint)]
        public void StopSpawn()
        {
            SpawnPoints.ForEach(p => p.Activated = false);
        }

        protected override void Deinitialize()
        {
        }
    }
}
