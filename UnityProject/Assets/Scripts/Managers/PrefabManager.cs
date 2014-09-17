using System;
using System.Collections.Generic;
using System.Diagnostics;
//using System.IO;
using System.Linq;
using Assets.Scripts.Constants;
using PathologicalGames;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    [AddComponentMenu("Manager/PrefabManager")]
    public class PrefabManager : MonoBehaviour
    {
        public int DefaultPreloadAmount;
        public bool DefaultCullDespawned;
        public int DefaultCullAbove;

        public static PrefabManager Instance;

        private Dictionary<string, SpawnPool> _prefabPoolMap;
        private Dictionary<GameObject, SpawnPool> _spawnedPrefabsMap;

        void Awake()
        {
            UpdateManager();

            _spawnedPrefabsMap = new Dictionary<GameObject, SpawnPool>();
            Instance = FindObjectOfType<PrefabManager>();
        }

        void OnDisable()
        {
            PoolManager.Pools.DestroyAll();
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
            _prefabPoolMap = new Dictionary<string, SpawnPool>();
            UpdateManagerHelper();
        }

        void UpdateManagerHelper(string assetDirectoryPath = PrefabConstants.StartingAssetPrefabPath, string resourcesPrefabPath = PrefabConstants.StartingResourcesPrefabPath)
        {
            //DirectoryInfo dir = new DirectoryInfo(assetDirectoryPath);

            //var files = dir.GetFiles("*.prefab").Where(f => (f.Extension == PrefabConstants.PrefabExtension)).ToList();
            //if (files.Any())
            //{
            //    SpawnPool spawnPool = CreateSpawnPool(dir.Name);
            //    List<string> prefabNames = new List<string>();
            //    foreach (var f in files)
            //    {
            //        prefabNames.Add(Path.GetFileNameWithoutExtension(f.Name));
            //        _prefabPoolMap.Add(resourcesPrefabPath + Path.GetFileNameWithoutExtension(f.Name), spawnPool);
            //    }
            //}

            //DirectoryInfo[] subDirectories = dir.GetDirectories();
            //foreach (var d in subDirectories)
            //{
            //    UpdateManagerHelper(assetDirectoryPath + d.Name + "/", resourcesPrefabPath + d.Name + "/");
            //}
        }

        SpawnPool CreateSpawnPool(string name)
        {
            SpawnPool spawnPool = PoolManager.Pools.Create(name);

            spawnPool.gameObject.transform.parent = transform;
            spawnPool.gameObject.transform.position = transform.position;
            spawnPool.gameObject.name = name + "SpawnPool";
            spawnPool.dontDestroyOnLoad = true;

            return spawnPool;
        }
    }
}
