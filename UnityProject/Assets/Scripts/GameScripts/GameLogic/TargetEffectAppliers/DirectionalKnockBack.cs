using Assets.Scripts.Constants;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers
{
    [AddComponentMenu("TargetEffectApplier/DirectionalKnockBack")]
    public class DirectionalKnockBack : TargetEffectApplier 
    {
        public float KnockBackSpeed;
        public float Time;

        protected override void ApplyEffect(GameObject target)
        {
            target.TriggerGameScriptEvent(GameScriptEvent.OnCharacterKnockBacked, TargetFinder.FinderPosition.Position, KnockBackSpeed, Time);
        }
    }
}
