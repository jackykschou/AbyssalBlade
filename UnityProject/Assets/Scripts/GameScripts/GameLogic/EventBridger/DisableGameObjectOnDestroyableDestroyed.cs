using UnityEngine;
using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;


namespace Assets.Scripts.GameScripts.GameLogic.EventBridger
{
    [AddComponentMenu("EventBridger/DisableGameObjectOnDestroyableDestroyed")]
    public class DisableGameObjectOnDestroyableDestroyed : GameLogic 
    {
        protected override void Deinitialize()
        {
        }

        [GameScriptEventAttribute(GameScriptEvent.OnDestroyableDestroyed)]
        public void OnDestroyableDestroyed()
        {
            DisableGameObject();
        }
    }
}
