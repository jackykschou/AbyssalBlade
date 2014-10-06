using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.CollideEffect.Effect
{
    public abstract class CollideTriggerEffect : GameLogic 
    {
        protected override void Deinitialize()
        {
        }

        [GameScriptEventAttribute(GameScriptEvent.OnCollideTriggerTriggered)]
        public abstract void OnCollideTriggerTriggered(GameObject target);
    }
}
