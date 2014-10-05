using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.Components.TimeDispatcher;
using Assets.Scripts.Managers;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Spawner
{
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(PrefabSpawner))]
    public class SpawnEnemy : GameLogic 
    {
        public PrefabSpawner PrefabSpawner;

        [Range(0f, float.MaxValue)] 
        public float SpawnRadius = 0f;

        public FixTimeDispatcher SpawnCoolDown;

        public CircleCollider2D TriggerArea;

        protected override void OnTriggerStay2D(Collider2D coll)
        {
            if (!PrefabSpawner.CanSpawn())
            {
                TriggerArea.enabled = false;
            }

            if (SpawnCoolDown.CanDispatch() &&
                LevelManager.Instance.PlayerMainCharacter != null &&
                !LevelManager.Instance.PlayerMainCharacter.HitPointAtZero())
            {
                SpawnCoolDown.Dispatch();
                Vector3 spawnPosition = new Vector3(Random.Range(transform.position.x - SpawnRadius, transform.position.x + SpawnRadius),
                    Random.Range(transform.position.y - SpawnRadius, transform.position.y + SpawnRadius), transform.position.z);
                PrefabSpawner.SpawnPrefab(spawnPosition);
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
            TriggerArea.enabled = true;
            gameObject.layer = LayerConstants.LayerMask.SpawnArea;
        }

        protected override void Deinitialize()
        {
        }
    }
}
