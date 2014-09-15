using Assets.Scripts.Constants;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.GameScripts.Components.PrefabLoader
{
    public class PrefabSpawner : GameScriptComponent
    {
        public Prefab Prefab;
        private GameObject _spawnedPrefab;

        public PrefabSpawner(GameObject spawnedPrefab)
        {
            _spawnedPrefab = spawnedPrefab;
        }

        public GameObject SpawnPrefab(Vector3 position)
        {
            _spawnedPrefab = PrefabManager.Instance.SpawnPrefab(Prefab, position);
            return _spawnedPrefab;
        }

        public void DespawnPrefab()
        {
            PrefabManager.Instance.DespawnPrefab(Prefab, _spawnedPrefab);
        }

        public override void Initialize()
        {
        }

        public override void Deinitialize()
        {
        }

        public override void Update()
        {
        }
    }
}
