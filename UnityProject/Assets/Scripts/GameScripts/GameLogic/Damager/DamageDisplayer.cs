using Assets.Scripts.Attributes;
using Assets.Scripts.GameScripts.Components.GameValue;
using UnityEngine;
using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;
using Assets.Scripts.Managers;


namespace Assets.Scripts.GameScripts.GameLogic.Damager
{
    public class DamageDisplayer : GameLogic
    {
        public Color textColor;

        [GameScriptEvent(Constants.GameScriptEvent.OnObjectTakeDamage)]
        public void TakeDamage(float damage)
        {
            GameObject obj = PrefabManager.Instance.SpawnPrefab(Constants.Prefab.DamageText, gameObject.transform.position);
            obj.GetComponent<TextMesh>().text = ((int)damage).ToString();
            obj.GetComponent<TextMesh>().color = textColor;
        }
        protected override void Deinitialize()
        {
        }
    }
}
