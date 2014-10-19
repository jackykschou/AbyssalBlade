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
            target.TriggerGameScriptEvent(GameScriptEvent.OnCharacterKnockBacked, MathUtility.GetDirection(TargetFinder.FinderPosition.Position.position, target.transform.position), KnockBackSpeed, Time);
        }
    }
}
