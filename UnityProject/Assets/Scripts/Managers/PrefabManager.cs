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

        private Dictionary<string, SpawnPool> _prefabPoolMap;
        private Dictionary<GameObject, SpawnPool> _spawnedPrefabsMap;

        void Awake()
        {
            _spawnedPrefabsMap = new Dictionary<GameObject, SpawnPool>();
            _prefabPoolMap = new Dictionary<string, SpawnPool>();
            Instance = GetComponent<PrefabManager>();
            CreateSpawnPools();
        }

        void CreateSpawnPools()
        {
            for(int i = 0; i < _serializedPrefabPoolMapKeys.Count; ++i)
            {
                SpawnPool spawnPool;
                if (_prefabPoolMap.Values.All(s => s.poolName != _serializedPrefabPoolMapValues[i]))
                {
                    spawnPool = PoolManager.Pools.Create(_serializedPrefabPoolMapValues[i]);
                    spawnPool.gameObject.transform.parent = transform;
                    spawnPool.gameObject.transform.position = transform.position;
                    spawnPool.gameObject.name = _serializedPrefabPoolMapValues[i];
                    spawnPool.dontDestroyOnLoad = true;

                    _prefabPoolMap.Add(_serializedPrefabPoolMapKeys[i], spawnPool);
                }
                else
                {
                    spawnPool =
                        _prefabPoolMap.Values.First(s => s.poolName == _serializedPrefabPoolMapValues[i]);
                    _prefabPoolMap.Add(_serializedPrefabPoolMapKeys[i], spawnPool);
                }

                if (_serializedPrefabPoolMapKeys[i].Contains(PreloadedPrefabFolderName))
                {
                    var o = Resources.Load(_serializedPrefabPoolMapKeys[i]) as GameObject;
                    PrefabPool prefabPool = new PrefabPool(o.transform);
                    prefabPool.preloadAmount = 30;

                    prefabPool.cullDespawned = true;
                    prefabPool.cullAbove = 40;
                    prefabPool.cullDelay = 5;

                    spawnPool.CreatePrefabPool(prefabPool);
                }
            }
        }

        public GameObject SpawnPrefab(Prefab prefab, Vector3 position)
        {
            string prefabName = PrefabConstants.GetPrefabName(prefab);
            if (!_prefabPoolMap.ContainsKey(prefabName))
            {
                throw new Exception("Prefab not found in _prefabPoolMap");
            }

            var o = Resources.Load(prefabName) as GameObject;
            if (o == null)
            {
                throw new Exception("Cannot load prefab " + prefabName);
            }

            GameObject spawned = _prefabPoolMap[prefabName].Spawn(o.transform, position, Quaternion.identity).gameObject;

            if(!_spawnedPrefabsMap.ContainsKey(spawned))
                _spawnedPrefabsMap.Add(spawned, _prefabPoolMap[prefabName]);

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
