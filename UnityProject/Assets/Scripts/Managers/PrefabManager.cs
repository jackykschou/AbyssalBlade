using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Constants;
using PathologicalGames;
using UnityEngine;

#if UNITY_EDITOR && !UNITY_WEBPLAYER
using System.IO;
#endif

namespace Assets.Scripts.Managers
{
    [AddComponentMenu("Manager/PrefabManager")] 
    public class PrefabManager : MonoBehaviour
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

        void Awake()
        {
            _spawnedPrefabsMap = new Dictionary<GameObject, SpawnPool>();
            _prefabPoolMap = new Dictionary<GameObject, SpawnPool>();
            _prefabNameMap = new Dictionary<string, GameObject>();
            Instance = GetComponent<PrefabManager>();
            CreateSpawnPools();
        }

        void CreateSpawnPools()
        {
            for(int i = 0; i < _serializedPrefabPoolMapKeys.Count; ++i)
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
                    PrefabPool prefabPool = new PrefabPool(obj.transform)
                    {
                        preloadAmount = 30,
                        cullDespawned = true,
                        cullAbove = 40,
                        cullDelay = 5
                    };

                    spawnPool.CreatePrefabPool(prefabPool);
                }
            }
        }

        public GameObject SpawnPrefab(Prefab prefab, Vector3 position)
        {
            string prefabName = PrefabConstants.GetPrefabName(prefab);
            if (!_prefabNameMap.ContainsKey(prefabName))
            {
                throw new Exception("Prefab not found in _prefabNameMap");
            }

            GameObject spawned = _prefabPoolMap[_prefabNameMap[prefabName]].Spawn(_prefabNameMap[prefabName].transform, position, Quaternion.identity).gameObject;

            if (!_spawnedPrefabsMap.ContainsKey(spawned))
            {
                _spawnedPrefabsMap.Add(spawned, _prefabPoolMap[_prefabNameMap[prefabName]]);
            }

            return spawned;
        }

        public void DespawnPrefab(GameObject prefabGameObject)
        {
            if (!IsSpawnedFromPrefab(prefabGameObject))
            {
                throw new Exception("object is not spawned by the manager");
            }

            _spawnedPrefabsMap[prefabGameObject].Despawn(prefabGameObject.transform);
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
