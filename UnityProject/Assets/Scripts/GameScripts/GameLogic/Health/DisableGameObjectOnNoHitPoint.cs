﻿using System.Collections;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Health
{
    [AddComponentMenu("HealthLogic/DisableGameObjectOnNoHitPoint")]
    public class DisableGameObjectOnNoHitPoint : GameLogic 
    {
        [Range(0f, float.MaxValue)]
        public float Delay = 1.0f;

        protected override void Deinitialize()
        {
        }

        [GameScriptEventAttribute(GameScriptEvent.OnObjectHasNoHitPoint)]
        public void DestroyGameObject()
        {
            StartCoroutine(DestroyGameObjectIE());
        }

        IEnumerator DestroyGameObjectIE()
        {
            yield return new WaitForSeconds(Delay);
            gameObject.SetActive(false);
        }
    }
}
