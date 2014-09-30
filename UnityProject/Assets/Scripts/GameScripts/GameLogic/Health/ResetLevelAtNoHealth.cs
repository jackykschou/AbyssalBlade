using System.Collections;
using Assets.Scripts.Managers;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Health
{
    [AddComponentMenu("HealthLogic/ResetLevelAtNoHealth")]
    public class ResetLevelAtNoHealth : GameLogic 
    {
        [Range(0f, float.MaxValue)]
        public float Delay = 1.0f;

        protected override void Deinitialize()
        {
        }

        [GameScriptEventAttribute(GameScriptEvent.OnOjectHasNoHitPoint)]
        public void DestroyGameObject()
        {
            StartCoroutine(DestroyGameObjectIE());
        }

        IEnumerator DestroyGameObjectIE()
        {
            yield return new WaitForSeconds(Delay);
            GameManager.Instance.ReloadLevel();
        }
    }
}
