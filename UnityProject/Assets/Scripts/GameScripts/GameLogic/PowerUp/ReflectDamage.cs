using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.GameValue;
using Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.PowerUp
{
    [AddComponentMenu("PowerUp/ReflectDamage")]
    [RequireComponent(typeof(HealthChanger))]
    public class ReflectDamage : PowerUp 
    {
        public HealthChanger HealthChanger;

        protected override void Initialize()
        {
            base.Initialize();
            if (HealthChanger == null)
            {
                HealthChanger = GetComponent<HealthChanger>();
            }
        }

        protected override void Deinitialize()
        {
        }


        [Attributes.GameScriptEvent(GameScriptEvent.OnObjectTakeDamage)]
        public void OnObjectTakeDamage(float amount, bool crited, GameValue.GameValue gameValue, GameValueChanger gameValueChanger)
        {
            HealthChanger.ApplierApplyEffect(gameValueChanger.Owner);
        }

        protected override void Apply()
        {
            HealthChanger.TriggerGameScriptEvent(GameScriptEvent.ChangeHealthChangerRawAmountToInitialPercentage, 1.0f + (AppliedCounter * 0.3f) - 0.3f);
        }

        protected override void UnApply()
        {
        }
    }
}
