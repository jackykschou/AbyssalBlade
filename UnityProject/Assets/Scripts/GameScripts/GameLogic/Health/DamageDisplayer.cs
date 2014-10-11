using Assets.Scripts.Attributes;
using Assets.Scripts.GameScripts.GameLogic.Spawner;
using UnityEngine;
using Assets.Scripts.Managers;

namespace Assets.Scripts.GameScripts.GameLogic.Health
{
    [RequireComponent(typeof(PrefabSpawner))]
    public class DamageDisplayer : GameLogic
    {
        public PrefabSpawner PrefabSpawner;
        public Color textColor;
        private bool WasCrit = false;

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
                if (WasCrit)
                {
                    textMesh.transform.localScale *= 1.5f;
                    textMesh.fontStyle = FontStyle.Italic;
                    WasCrit = false;
                    //MessageManager.Instance.DisplayMessage("CRIT!",Vector3.up);
                }
                else
                {
                    textMesh.fontStyle = FontStyle.Normal;
                }
            });
        }

        [GameScriptEvent(Constants.GameScriptEvent.OnObjectWasCrit)]
        public void SetWasCrit()
        {
            WasCrit = true;
        }

        protected override void Deinitialize()
        {
        }
    }
}
