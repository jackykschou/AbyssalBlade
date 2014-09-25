using System.Collections.Generic;
using Assets.Scripts.Constants;
using Assets.Scripts.Managers;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Spawner
{
    public class PrefabSpawner : GameLogic
    {
        public List<Prefab> Prefabs;
        public List<float> SpawnPickWeights;
        [Range(0, 5000)]
        public int NumberOfSpawn = 1;
        [Range(0f, 1.0f)] 
        public float SpawnChance = 1.0f;

        private int _spawnCount;
        private List<ProportionValue<Prefab>> _prefabWeightMap;

        protected override void Initialize()
        {
            base.Initialize();
            _spawnCount = 0;
            _prefabWeightMap = new List<ProportionValue<Prefab>>();
            for (int i = 0; i < Prefabs.Count; ++i)
            {
                _prefabWeightMap.Add(ProportionValue.Create(SpawnPickWeights[i], Prefabs[i]));
            }
            float sum = 0f;
            SpawnPickWeights.ForEach(w => sum += w);
            if (!Mathf.Approximately(sum, 1.0f))
            {
                Debug.LogError("The sum of weight in PrefabSpawner of " + gameObject.name + " is not equal to one.");
                for (int i = 0; i < SpawnPickWeights.Count; ++i)
                {
                    SpawnPickWeights[i] = 0f;
                }
            }
        }

        public override void EditorUpdate()
        {
            base.EditorUpdate();
            if (Prefabs.Count != SpawnPickWeights.Count)
            {
                SpawnPickWeights.Resize(Prefabs.Count);
            }
        }

        public void SpawnPrefab(Vector3 position)
        {
            if (_spawnCount > NumberOfSpawn)
            {
                return;
            }

            if (SpawnChance >= Random.value)
            {
                PrefabManager.Instance.SpawnPrefab(_prefabWeightMap.ChooseByRandom(), position);
            }

            _spawnCount++;
        }

        protected override void Deinitialize()
        {
        }
    }
}
