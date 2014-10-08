using System.Collections;
using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.Components.TimeDispatcher;
using Assets.Scripts.GameScripts.GameLogic.Spawner;
using Assets.Scripts.Managers;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.LevelMechanics.Section
{
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(PrefabSpawner))]
    [AddComponentMenu("LevelMechanics/Section/SectionEnemySpawnPoint")]
    public class SectionEnemySpawnPoint : SectionLogic 
    {
        public PrefabSpawner PrefabSpawner;

        [Range(0f, float.MaxValue)] 
        public float SpawnRadius = 0f;

        public FixTimeDispatcher SpawnCoolDown;

        public CircleCollider2D TriggerArea;

        public bool Activated = true;

        public bool IsActive 
        {
            get { return PrefabSpawner.CanSpawn() && Activated && SectionActivated; }
        }

        public override void OnSectionActivated(int sectionId)
        {
            base.OnSectionActivated(sectionId);
            if (sectionId == SectionId)
            {
                TriggerGameEvent(GameEvent.OnSectionEnemySpawnPointActivated, gameObject, SectionId);
            }
        }

        public override void OnSectionDeactivated(int sectionId)
        {
            base.OnSectionDeactivated(sectionId);
            if (sectionId == SectionId)
            {
                TriggerGameEvent(GameEvent.OnSectionEnemySpawnPointDeactivated, gameObject, SectionId);
            }
        }

        protected override void OnTriggerStay2D(Collider2D coll)
        {
            if (SpawnCoolDown.CanDispatch() &&
                LevelManager.Instance.PlayerMainCharacter != null &&
                !LevelManager.Instance.PlayerMainCharacter.HitPointAtZero())
            {
                StartCoroutine(SpawnEnemy());
            }
        }

        public IEnumerator SpawnEnemy()
        {
            SpawnCoolDown.Dispatch();
            Vector3 spawnPosition = new Vector3(Random.Range(transform.position.x - SpawnRadius, transform.position.x + SpawnRadius),
                Random.Range(transform.position.y - SpawnRadius, transform.position.y + SpawnRadius), transform.position.z);
            GameObject spawnedEnemy = PrefabSpawner.SpawnPrefab(spawnPosition);
            while (spawnedEnemy == null || !spawnedEnemy.activeSelf)
            {
                yield return new WaitForSeconds(Time.deltaTime);
            }
            spawnedEnemy.tag = TagConstants.EnemyTag;
            var despawnedOnNoHitPoint = spawnedEnemy.GetComponent<TriggerOnSectionEnemyDespawnedOnNoHitPoint>();
            var noHitPointOnSectionDeactivated = spawnedEnemy.GetComponent<TriggerNoHitPointOnSectionDeactivated>();
            if (despawnedOnNoHitPoint == null)
            {
                despawnedOnNoHitPoint = spawnedEnemy.AddComponent<TriggerOnSectionEnemyDespawnedOnNoHitPoint>();
            }
            if (noHitPointOnSectionDeactivated == null)
            {
                noHitPointOnSectionDeactivated = spawnedEnemy.AddComponent<TriggerNoHitPointOnSectionDeactivated>();
            }
            despawnedOnNoHitPoint.SectionId = SectionId;
            noHitPointOnSectionDeactivated.SectionId = SectionId;
            TriggerGameEvent(GameEvent.OnSectionEnemySpawned, spawnedEnemy, SectionId);
        }

        protected override void Update()
        {
            base.Update();
            if (SectionActivated && PrefabSpawner.CanSpawn() && Activated)
            {
                TriggerArea.enabled = true;
            }
            else if (!PrefabSpawner.CanSpawn() || !Activated || !SectionActivated)
            {
                TriggerArea.enabled = false;
            }
        }

        protected override void Initialize()
        {
            base.Initialize();
            if (PrefabSpawner == null)
            {
                PrefabSpawner = GetComponent<PrefabSpawner>();
            }
            TriggerArea = GetComponent<CircleCollider2D>();
            TriggerArea.isTrigger = true;
            TriggerArea.enabled = false;
            gameObject.layer = LayerConstants.LayerMask.SpawnArea;
            Activated = true;
        }

        protected override void Deinitialize()
        {
        }
    }
}
