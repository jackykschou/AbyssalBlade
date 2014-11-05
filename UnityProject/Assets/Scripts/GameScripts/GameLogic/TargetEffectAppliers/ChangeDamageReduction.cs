using Assets.Scripts.Constants;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers
{
    [AddComponentMenu("TargetEffectApplier/ChangeDamageReduction")]
    public class ChangeDamageReduction : TargetEffectApplier
    {
        public float ChangeAmount;

        protected override void ApplyEffect(GameObject target)
        {
            target.TriggerGameScriptEvent(GameScriptEvent.ChangeDamageReduction, ChangeAmount);
        }
    }
}
