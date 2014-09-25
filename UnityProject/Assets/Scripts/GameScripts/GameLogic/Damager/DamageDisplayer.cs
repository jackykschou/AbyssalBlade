using Assets.Scripts.Attributes;
using Assets.Scripts.GameScripts.GameLogic.Spawner;
using UnityEngine;
using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;
using Assets.Scripts.Managers;

namespace Assets.Scripts.GameScripts.GameLogic.Damager
{
    [RequireComponent(typeof(PrefabSpawner))]
    public class DamageDisplayer : GameLogic
    {
        public PrefabSpawner PrefabSpawner;
        public Color textColor;

        protected override void Initialize()
        {
            base.Initialize();
            if (PrefabSpawner == null)
            {
                PrefabSpawner = GetComponent<PrefabSpawner>();
            }
        }

        [GameScriptEvent(Constants.GameScriptEvent.OnObjectTakeDamage)]
        public void TakeDamage(float damage)
        {
            TextMesh textMesh = PrefabSpawner.SpawnPrefab(transform.position).GetComponent<TextMesh>();
            textMesh.text = ((int)damage).ToString();
            textMesh.color = textColor;
        }
        protected override void Deinitialize()
        {
        }
    }
}
