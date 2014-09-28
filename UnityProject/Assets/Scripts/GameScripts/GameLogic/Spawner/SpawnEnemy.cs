using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.Components.TimeDispatcher;
using Assets.Scripts.Managers;
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

        [Range(0f, float.MaxValue)] 
        public float ActivateDistance = 15f;

        public FixTimeDispatcher SpawnCoolDown;

        public CircleCollider2D Collider;

        protected override void OnTriggerStay2D(Collider2D coll)
        {
            if (!PrefabSpawner.CanSpawn())
            {
                Collider.enabled = false;
            }

            if (SpawnCoolDown.CanDispatch() &&
                LevelManager.Instance.PlayerMainCharacter != null)
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
            Collider = GetComponent<CircleCollider2D>();
            Collider.isTrigger = true;
            Collider.radius = ActivateDistance;
            gameObject.layer = LayerConstants.LayerMask.SpawnArea;
        }

        protected override void Deinitialize()
        {
        }
    }
}
