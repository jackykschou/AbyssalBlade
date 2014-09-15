using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.Scripts.Constants;
using PathologicalGames;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    [ExecuteInEditMode]
    public class PrefabManager : MonoBehaviour
    {
        public int DefaultPreloadAmount;
        public bool DefaultCullDespawned;
        public int DefaultCullAbove;

        public static PrefabManager Instance;
        [SerializeField] 
        private Dictionary<string, List<string>> _prefabNameMap;
        [SerializeField] 
        private Dictionary<string, SpawnPool> _prefabPoolMap;

        void Awake()
        {
            Instance = FindObjectOfType<PrefabManager>();
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

            return _prefabPoolMap[prefabName].Spawn(o.transform, position, Quaternion.identity).gameObject;
        }

        public void DespawnPrefab(Prefab prefab, GameObject prefabGameObject)
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

            _prefabPoolMap[prefabName].Despawn(prefabGameObject.transform);
        }

        public void UpdateManager()
        {
            foreach (Transform c in transform)
            {
                DestroyImmediate(c.gameObject);
            }
            _prefabNameMap = new Dictionary<string, List<string>>();
            _prefabPoolMap = new Dictionary<string, SpawnPool>();
            UpdateManagerHelper();
        }

        void UpdateManagerHelper(string assetDirectoryPath = PrefabConstants.StartingAssetPrefabPath, string resourcesPrefabPath = PrefabConstants.StartingResourcesPrefabPath)
        {
            DirectoryInfo dir = new DirectoryInfo(assetDirectoryPath);

            var files = dir.GetFiles("*.prefab").Where(f => (f.Extension == PrefabConstants.PrefabExtension)).ToList();
            if (files.Any())
            {
                SpawnPool spawnPool = CreateSpawnPool(dir.Name);
                List<string> prefabNames = new List<string>();
                foreach (var f in files)
                {
                    prefabNames.Add(Path.GetFileNameWithoutExtension(f.Name));
                    _prefabPoolMap.Add(resourcesPrefabPath + Path.GetFileNameWithoutExtension(f.Name), spawnPool);
                }
                _prefabNameMap.Add(resourcesPrefabPath, prefabNames);
            }

            DirectoryInfo[] subDirectories = dir.GetDirectories();
            foreach (var d in subDirectories)
            {
                UpdateManagerHelper(assetDirectoryPath + d.Name, resourcesPrefabPath + d.Name);
            }
        }

        SpawnPool CreateSpawnPool(string name)
        {
            SpawnPool spawnPool = PoolManager.Pools.Create(name + "SpawnPool");

            spawnPool.gameObject.transform.parent = transform;
            spawnPool.gameObject.transform.position = transform.position;
            spawnPool.gameObject.name = name + "SpawnPool";
            spawnPool.poolName = name + "SpawnPool";
            spawnPool.dontDestroyOnLoad = true;

            return spawnPool;
        }
    }
}
