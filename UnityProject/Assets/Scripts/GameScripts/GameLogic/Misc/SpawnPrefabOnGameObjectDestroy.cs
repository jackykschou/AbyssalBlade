﻿using Assets.Scripts.Constants;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Misc
{
    [AddComponentMenu("Misc/SpawnPrefabOnGameObjectDestroy")]
    public class SpawnPrefabOnGameObjectDestroy : MonoBehaviour
    {
        public Prefab Prefab;

        [Attributes.GameScriptEvent(GameScriptEvent.OnObjectDestroyed)]
        public void OnObjectDestroyed()
        {
            PrefabManager.Instance.SpawnPrefabImmediate(Prefab, transform.position);
        }
    }
}
