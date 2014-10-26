using System.Collections;
using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.LevelMechanics.Section;
using Assets.Scripts.GameScripts.GameLogic.Spawner;
using Assets.Scripts.Managers;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects.SpawnEffect
{
    [RequireComponent(typeof(PrefabSpawner))]
    [AddComponentMenu("Skill/SkillEffect/SpawnSectionEnemy")]
    public class SpawnSectionEnemy : SkillEffect 
    {
        public PrefabSpawner PrefabSpawner;

        [Range(0f, 100f)]
        public float SpawnRadius = 0f;

        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            if (PrefabSpawner == null)
            {
                PrefabSpawner = GetComponent<PrefabSpawner>();
            }
        }

        public override void Activate()
        {
            base.Activate();
            StartCoroutine(StartSpawn());
            Activated = false;
        }

        public IEnumerator StartSpawn()
        {
            Vector3 spawnPosition = new Vector3(Random.Range(transform.position.x - SpawnRadius, transform.position.x + SpawnRadius),
                Random.Range(transform.position.y - SpawnRadius, transform.position.y + SpawnRadius), transform.position.z);
            while (!UtilityFunctions.LocationPathFindingReachable(transform.position, spawnPosition))
            {
                spawnPosition = new Vector3(Random.Range(transform.position.x - SpawnRadius, transform.position.x + SpawnRadius),
                Random.Range(transform.position.y - SpawnRadius, transform.position.y + SpawnRadius), transform.position.z);
                yield return new WaitForSeconds(0f);
            }
            PrefabSpawner.SpawnPrefab(spawnPosition, o =>
            {
                var triggerNoHitPointOnSectionDeactivated = o.GetComponent<TriggerNoHitPointOnSectionDeactivated>() ??
                                                            o.AddComponent<TriggerNoHitPointOnSectionDeactivated>();
                var triggerOnSectionEnemyDespawnedOnNoHitPoint = o.GetComponent<TriggerOnSectionEnemyDespawnedOnNoHitPoint>() ??
                                                                 o.AddComponent<TriggerOnSectionEnemyDespawnedOnNoHitPoint>();
                triggerNoHitPointOnSectionDeactivated.SectionId = LevelManager.Instance.CurrentSectionId;
                triggerOnSectionEnemyDespawnedOnNoHitPoint.SectionId = LevelManager.Instance.CurrentSectionId;
            });
            TriggerGameEvent(GameEvent.OnSectionEnemySpawned, LevelManager.Instance.CurrentSectionId);
        }
    }
}
