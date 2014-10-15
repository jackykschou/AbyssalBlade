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

        private bool _reset = false;

        protected override void Initialize()
        {
            base.Initialize();
            _reset = false;
        }

        protected override void Deinitialize()
        {
        }

        [GameScriptEventAttribute(GameScriptEvent.OnObjectHasNoHitPoint)]
        public void ResetLevel()
        {
            if (_reset)
            {
                return;
            }
            _reset = true;
            StartCoroutine(ResetLevelIE());
        }

        IEnumerator ResetLevelIE()
        {
            yield return new WaitForSeconds(Delay);
            GameManager.Instance.ReloadLevel();
        }
    }
}
