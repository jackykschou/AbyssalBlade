﻿using System;
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
        public bool LimitNumberOfSpawn = false;
        [Range(0, 100000)]
        public int NumberOfSpawn = 1;
        [Range(0f, 1.0f)] 
        public float SpawnChance = 1.0f;

        private int _spawnCount;
        private List<ProportionValue<Prefab>> _prefabWeightMap;

        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
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

        protected override void Initialize()
        {
            base.Initialize();
            _spawnCount = 0;
        }

        public override void EditorUpdate()
        {
            base.EditorUpdate();
            if (Prefabs == null)
            {
                return;
            }

            if (Prefabs.Count != SpawnPickWeights.Count)
            {
                SpawnPickWeights.Resize(Prefabs.Count);
            }
        }

        public bool CanSpawn()
        {
            return !(_spawnCount >= NumberOfSpawn && LimitNumberOfSpawn);
        }

        public void SpawnPrefab(Action<GameObject> onPrefabSpawned = null)
        {
            if (!CanSpawn())
            {
                return;
            }

            _spawnCount++;

            if (UtilityFunctions.RollChance(SpawnChance))
            {
                PrefabManager.Instance.SpawnPrefab(_prefabWeightMap.ChooseByRandom(), onPrefabSpawned);
            }
        }

        public void SpawnPrefab(Vector3 position, Action<GameObject> onPrefabSpawned = null)
        {
            if (_spawnCount >= NumberOfSpawn && LimitNumberOfSpawn)
            {
                return;
            }

            _spawnCount++;

            if (UtilityFunctions.RollChance(SpawnChance))
            {
                PrefabManager.Instance.SpawnPrefab(_prefabWeightMap.ChooseByRandom(), position, onPrefabSpawned);
            }
        }

        public void SpawnPrefabImmediate(Action<GameObject> onPrefabSpawned = null)
        {
            if (_spawnCount >= NumberOfSpawn && LimitNumberOfSpawn)
            {
                return;
            }

            _spawnCount++;

            if (UtilityFunctions.RollChance(SpawnChance))
            {
                PrefabManager.Instance.SpawnPrefabImmediate(_prefabWeightMap.ChooseByRandom(), onPrefabSpawned);
            }
        }

        public void SpawnPrefabImmediate(Vector3 position, Action<GameObject> onPrefabSpawned = null)
        {
            if (_spawnCount >= NumberOfSpawn && LimitNumberOfSpawn)
            {
                return;
            }

            _spawnCount++;

            if (UtilityFunctions.RollChance(SpawnChance))
            {
                PrefabManager.Instance.SpawnPrefabImmediate(_prefabWeightMap.ChooseByRandom(), position, onPrefabSpawned);
            }
        }

        protected override void Deinitialize()
        {
        }
    }
}
