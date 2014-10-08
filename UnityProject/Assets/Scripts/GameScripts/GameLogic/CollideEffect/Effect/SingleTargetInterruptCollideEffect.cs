using Assets.Scripts.Constants;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.CollideEffect.Effect
{
    [AddComponentMenu("CollideEffectTrigger/Effect/SingleTargetInterruptCollideEffect")]
    public class SingleTargetInterruptCollideEffect : CollideTriggerEffect 
    {
        public override void OnCollideTriggerTriggered(GameObject target)
        {
            target.TriggerGameScriptEvent(GameScriptEvent.InterruptCharacter);
        }
    }
}
