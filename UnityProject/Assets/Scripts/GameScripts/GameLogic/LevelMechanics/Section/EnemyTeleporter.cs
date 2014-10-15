using System.Collections.Generic;
using Assets.Scripts.Attributes;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.LevelMechanics.Section
{
    [RequireComponent(typeof(Health.Health))]
    [AddComponentMenu("LevelMechanics/Section/EnemyTeleporter")]
    public class EnemyTeleporter : SectionLogic
    {
        public List<SectionEnemySpawnPoint> SpawnPoints;

        private Health.Health _health;

        [GameScriptEvent(Constants.GameScriptEvent.OnObjectHasNoHitPoint)]
        public void StopSpawn()
        {
            SpawnPoints.ForEach(p => p.Activated = false);
        }

        public override void OnSectionActivated(int sectionId)
        {
            base.OnSectionActivated(sectionId);
            if (sectionId == SectionId)
            {
                _health.Invincible = false;
            }
        }

        public override void OnSectionDeactivated(int sectionId)
        {
            base.OnSectionDeactivated(sectionId);
            if (sectionId == SectionId)
            {
                _health.Invincible = true;
            }
        }

        protected override void Initialize()
        {
            base.Initialize();
            _health = GetComponent<Health.Health>();
            _health.Invincible = true;
        }

        protected override void Deinitialize()
        {
        }
    }
}
