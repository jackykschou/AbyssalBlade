using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.GameValue;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers
{
    [RequireComponent(typeof(GameValueChanger))]
    [AddComponentMenu("TargetEffectApplier/HealthChanger")]
    public class HealthChanger : TargetEffectApplier
    {
        public GameValueChanger GameValueChanger;

        protected override void ApplyEffect(GameObject target)
        {
            ApplyHealthChange(target);
        }

        private void ApplyHealthChange(GameObject target)
        {
            Debug.Log("ApplyHealthChangeApplyHealthChangeApplyHealthChange");
            target.TriggerGameScriptEvent(GameScriptEvent.ObjectChangeHealth, GameValueChanger);
        }

        protected override void Deinitialize()
        {
        }
    }
}
