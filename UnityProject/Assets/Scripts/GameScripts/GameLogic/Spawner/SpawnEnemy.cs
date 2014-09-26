using Assets.Scripts.GameScripts.Components.TimeDispatcher;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Spawner
{
    [RequireComponent(typeof(PrefabSpawner))]
    public class SpawnEnemy : GameLogic 
    {
        public PrefabSpawner PrefabSpawner;

        [Range(0f, float.MaxValue)] 
        public float SpawnRadius = 0f;

        [Range(0f, float.MaxValue)] 
        public float ActivateDistance = 15f;

        public FixTimeDispatcher SpawnCoolDown;

        protected override void Update()
        {
            base.Update();
            if (LevelManager.Instance.PlayerMainCharacter != null && 
                (Vector2.Distance(LevelManager.Instance.PlayerMainCharacter.transform.position, transform.position) <= ActivateDistance) &&
                SpawnCoolDown.CanDispatch())
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
        }

        protected override void Deinitialize()
        {
        }
    }
}
