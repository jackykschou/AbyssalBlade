using Assets.Scripts.GameScripts.GameLogic.Health;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.CollideEffect.Effect
{
    [RequireComponent(typeof(HealthChanger))]
    public class HealthChangeCollideEffect : CollideTriggerEffect 
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

        public override void OnCollideTriggerTriggered(GameObject target)
        {
            HealthChanger.ApplyHealthChange(target);
        }
    }
}
