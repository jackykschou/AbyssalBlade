using Assets.Scripts.Constants;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.GameScripts.Components.PrefabLoader
{
    public class PrefabSpawner : GameScriptComponent
    {
        public Prefab Prefab;

        public void SpawnPrefab(Vector3 position)
        {
            PrefabManager.Instance.SpawnPrefab(Prefab, position);
        }

        public override void Initialize()
        {
        }

        public override void Deinitialize()
        {
        }
    }
}
