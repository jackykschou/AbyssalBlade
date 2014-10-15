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

        protected override void Initialize()
        {
            base.Initialize();
            if (PrefabSpawner == null)
            {
                PrefabSpawner = GetComponent<PrefabSpawner>();
            }
        }

        [GameScriptEvent(Constants.GameScriptEvent.OnObjectTakeDamage)]
        public void TakeDamage(float damage, bool crit)
        {
            PrefabSpawner.SpawnPrefabImmediate(transform.position, o =>
            {
                TextMesh textMesh = o.GetComponent<TextMesh>();
                textMesh.text = ((int)damage).ToString();
                textMesh.color = textColor;
                if (crit)
                {
                    textMesh.transform.localScale *= 1.5f;
                    textMesh.fontStyle = FontStyle.Italic;
                    //MessageManager.Instance.DisplayMessage("CRIT!",Vector3.up);
                }
                else
                {
                    textMesh.fontStyle = FontStyle.Normal;
                }
            });
        }

        protected override void Deinitialize()
        {
        }
    }
}
