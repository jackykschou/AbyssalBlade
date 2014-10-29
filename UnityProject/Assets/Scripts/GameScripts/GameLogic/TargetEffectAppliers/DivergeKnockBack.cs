using Assets.Scripts.Constants;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers
{
    [AddComponentMenu("TargetEffectApplier/DivergeKnockBack")]
    public class DivergeKnockBack : TargetEffectApplier 
    {
        public float KnockBackSpeed;
        public float Time;

        protected override void ApplyEffect(GameObject target)
        {
            target.TriggerGameScriptEvent(GameScriptEvent.OnCharacterKnockBacked, UtilityFunctions.GetDirection(TargetFinder.FinderPosition.Position.position, target.transform.position).normalized, KnockBackSpeed, Time);
        }
    }
}
