﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic;
using Assets.Scripts.GameScripts.GameLogic.Health;
using PathologicalGames;
using UnityEngine;

#if UNITY_EDITOR && !UNITY_WEBPLAYER
using System.IO;
#endif

namespace Assets.Scripts.Managers
{
    [AddComponentMenu("Manager/PrefabManager")] 
    public class PrefabManager : GameLogic
    {
        public const string PreloadedPrefabFolderName = "PreloadedPrefab";

        public static PrefabManager Instance;

        [SerializeField]
        private List<string> _serializedPrefabPoolMapKeys;
        [SerializeField]
        private List<string> _serializedPrefabPoolMapValues;

        private Dictionary<string, GameObject> _prefabNameMap;
        private Dictionary<GameObject, SpawnPool> _prefabPoolMap;
        private Dictionary<GameObject, SpawnPool> _spawnedPrefabsMap;

        private List<GameObject> _despawnQueue;
        private List<Action<GameObject>> _despawnDelegateQueue;
        private List<Prefab> _spawnQueue;
        private List<Vector3> _spawnPositionQueue;
        private List<Action<GameObject>> _spawnDelegateQueue;

        void CreateSpawnPools()
        {
            for (int i = 0; i < _serializedPrefabPoolMapKeys.Count; ++i)
            {
                SpawnPool spawnPool;
                GameObject obj = Resources.Load(_serializedPrefabPoolMapKeys[i]) as GameObject;

                if (obj == null)
                {
                    throw new Exception("Object is not a prefab");
                }

                if (_prefabPoolMap.Values.All(s => s.poolName != _serializedPrefabPoolMapValues[i]))
                {
                    spawnPool = PoolManager.Pools.Create(_serializedPrefabPoolMapValues[i]);
                    spawnPool.gameObject.transform.parent = transform;
                    spawnPool.gameObject.transform.position = transform.position;
                    spawnPool.gameObject.name = _serializedPrefabPoolMapValues[i];
                    spawnPool.dontDestroyOnLoad = true;

                    _prefabPoolMap.Add(obj, spawnPool);
                }
                else
                {
                    spawnPool =
                        _prefabPoolMap.Values.First(s => s.poolName == _serializedPrefabPoolMapValues[i]);
                    _prefabPoolMap.Add(obj, spawnPool);
                }

                _prefabNameMap.Add(_serializedPrefabPoolMapKeys[i], obj);

                if (_serializedPrefabPoolMapKeys[i].Contains(PreloadedPrefabFolderName))
                {
                    if (obj.GetComponent<DestroyOnLevelEnded>() == null)
                    {
                        obj.AddComponent<DestroyOnLevelEnded>();
                    }

                    PrefabPool prefabPool = new PrefabPool(obj.transform)
                    {
                        preloadAmount = 5,
                        cullDespawned = true,
                        cullAbove = 200,
                        cullDelay = 5,
                        limitInstances = false,
                        limitFIFO = true
                    };

                    spawnPool.CreatePrefabPool(prefabPool);
                }
            }
        }

        protected override void Initialize()
        {
            base.Initialize();
            _spawnedPrefabsMap = new Dictionary<GameObject, SpawnPool>();
            _prefabPoolMap = new Dictionary<GameObject, SpawnPool>();
            _prefabNameMap = new Dictionary<string, GameObject>();
            _despawnQueue = new List<GameObject>();
            _despawnDelegateQueue = new List<Action<GameObject>>();
            _spawnQueue = new List<Prefab>();
            _spawnPositionQueue = new List<Vector3>();
            _spawnDelegateQueue = new List<Action<GameObject>>();
            Instance = GetComponent<PrefabManager>();
            CreateSpawnPools();
        }

        protected override void Deinitialize()
        {
        }

        private void SpawnHelper(Prefab prefab, Vector2 position, Action<GameObject> onPrefabSpawned = null)
        {
            string prefabName = PrefabConstants.GetPrefabName(prefab);
            GameObject prefabGameObject = _prefabNameMap[prefabName];

            GameObject spawned = _prefabPoolMap[prefabGameObject].Spawn(prefabGameObject.transform, new Vector3(position.x, position.y, prefabGameObject.transform.position.z), Quaternion.identity).gameObject;

            if (onPrefabSpawned != null)
            {
                onPrefabSpawned(spawned);
            }

            _spawnedPrefabsMap.Add(spawned, _prefabPoolMap[_prefabNameMap[prefabName]]);
        }

        public void SpawnPrefab(Prefab prefab, Vector2 position, Action<GameObject> onPrefabSpawned = null)
        {
            if (_spawnQueue.Count == 0)
            {
                _spawnQueue.Add(prefab);
                _spawnPositionQueue.Add(position);
                _spawnDelegateQueue.Add(onPrefabSpawned);
                StartCoroutine(SpawnPrefabIE());
            }
            else
            {
                _spawnQueue.Add(prefab);
                _spawnPositionQueue.Add(position);
                _spawnDelegateQueue.Add(onPrefabSpawned);
            }
        }

        public IEnumerator SpawnPrefabIE()
        {
            while (_spawnQueue.Count > 0)
            {
                Prefab prefab = _spawnQueue.First();
                _spawnQueue.RemoveAt(0);
                Vector2 position = _spawnPositionQueue.First();
                _spawnPositionQueue.RemoveAt(0);
                Action<GameObject> onPrefabSpawned = _spawnDelegateQueue.First();
                _spawnDelegateQueue.RemoveAt(0);

                string prefabName = PrefabConstants.GetPrefabName(prefab);
                GameObject prefabGameObject = _prefabNameMap[prefabName];

                GameObject spawned = _prefabPoolMap[prefabGameObject].Spawn(prefabGameObject.transform, new Vector3(position.x, position.y, prefabGameObject.transform.position.z), Quaternion.identity).gameObject;

                if (onPrefabSpawned != null)
                {
                    onPrefabSpawned(spawned);
                }

                _spawnedPrefabsMap.Add(spawned, _prefabPoolMap[_prefabNameMap[prefabName]]);

                yield return new WaitForSeconds(0.2f);
            }
        }

        public void SpawnPrefab(Prefab prefab, Action<GameObject> onPrefabSpawned = null)
        {
            string prefabName = PrefabConstants.GetPrefabName(prefab);
            GameObject prefabGameObject = _prefabNameMap[prefabName];

            SpawnPrefab(prefab, prefabGameObject.transform.position, onPrefabSpawned);
        }

        public void SpawnPrefabImmediate(Prefab prefab, Vector2 position, Action<GameObject> onPrefabSpawned = null)
        {
            SpawnHelper(prefab, position, onPrefabSpawned);
        }

        public void SpawnPrefabImmediate(Prefab prefab, Action<GameObject> onPrefabSpawned = null)
        {
            string prefabName = PrefabConstants.GetPrefabName(prefab);
            GameObject prefabGameObject = _prefabNameMap[prefabName];
            SpawnHelper(prefab, prefabGameObject.transform.position, onPrefabSpawned);
        }

        public void DespawnPrefab(GameObject prefabGameObject, Action<GameObject> onPrefabDespawned = null)
        {
            if (_despawnQueue.Count == 0)
            {
                _despawnQueue.Add(prefabGameObject);
                _despawnDelegateQueue.Add(onPrefabDespawned);
                StartCoroutine(DespawnPrefabIE());
            }
            else
            {
                _despawnQueue.Add(prefabGameObject);
                _despawnDelegateQueue.Add(onPrefabDespawned);
            }
        }

        public IEnumerator DespawnPrefabIE()
        {
            while (_despawnQueue.Count > 0)
            {
                GameObject prefabGameObject = _despawnQueue.First();
                _despawnQueue.RemoveAt(0);
                Action<GameObject> onPrefabDespawned = _despawnDelegateQueue.First();
                _despawnDelegateQueue.RemoveAt(0);

                if (onPrefabDespawned != null)
                {
                    onPrefabDespawned(prefabGameObject);
                }
                _spawnedPrefabsMap[prefabGameObject].Despawn(prefabGameObject.transform);
                _spawnedPrefabsMap.Remove(prefabGameObject);

                yield return new WaitForSeconds(0.2f);
            }
        }

        public void ImmediateDespawnPrefab(GameObject prefabGameObject, Action<GameObject> onPrefabDespawned = null)
        {
            if (onPrefabDespawned != null)
            {
                onPrefabDespawned(prefabGameObject);
            }
            if (_spawnedPrefabsMap.ContainsKey(prefabGameObject))
            {
                _spawnedPrefabsMap[prefabGameObject].Despawn(prefabGameObject.transform);
                _spawnedPrefabsMap.Remove(prefabGameObject);
            }
        }

        public bool IsSpawnedFromPrefab(GameObject obj)
        {
            return _spawnedPrefabsMap.ContainsKey(obj);
        }

        public void UpdateManager()
        {
#if UNITY_WEBPLAYER
            Debug.LogWarning("PrefabManager's Update is not functional in WebPlayer");
            return;
#endif
            _serializedPrefabPoolMapKeys = new List<string>();
            _serializedPrefabPoolMapValues = new List<string>();
            UpdateManagerHelper();
        }

        void UpdateManagerHelper(string assetDirectoryPath = PrefabConstants.StartingAssetPrefabPath, string resourcesPrefabPath = PrefabConstants.StartingResourcesPrefabPath)
        {
#if UNITY_EDITOR && !UNITY_WEBPLAYER
            DirectoryInfo dir = new DirectoryInfo(assetDirectoryPath);

            var files = dir.GetFiles("*.prefab").Where(f => (f.Extension == PrefabConstants.PrefabExtension)).ToList();
            if (files.Any())
            {
                string poolName = dir.Name;
                foreach (var f in files)
                {
                    _serializedPrefabPoolMapKeys.Add(resourcesPrefabPath + Path.GetFileNameWithoutExtension(f.Name));
                    _serializedPrefabPoolMapValues.Add(poolName);
                }
            }

            DirectoryInfo[] subDirectories = dir.GetDirectories();
            foreach (var d in subDirectories)
            {
                UpdateManagerHelper(assetDirectoryPath + d.Name + "/", resourcesPrefabPath + d.Name + "/");
            }
#endif
        }
    }
}
