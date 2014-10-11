using Assets.Scripts.Attributes;
using Assets.Scripts.GameScripts.GameLogic.Spawner;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Health
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
            PrefabSpawner.SpawnPrefabImmediate(transform.position, o =>
            {
                TextMesh textMesh = o.GetComponent<TextMesh>();
                textMesh.text = ((int)damage).ToString();
                textMesh.color = textColor;
            });
        }
        protected override void Deinitialize()
        {
        }
    }
}
