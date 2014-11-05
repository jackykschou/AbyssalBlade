using Assets.Scripts.Constants;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers
{
    [AddComponentMenu("TargetEffectApplier/ChangeHealthChangerDamageCriticalChance")]
    public class ChangeHealthChangerDamageCriticalChance : TargetEffectApplier
    {
        public float ChangeAmount;

        protected override void ApplyEffect(GameObject target)
        {
            target.TriggerGameScriptEvent(GameScriptEvent.ChangeDamageCriticalChance, ChangeAmount);
        }
    }
}
