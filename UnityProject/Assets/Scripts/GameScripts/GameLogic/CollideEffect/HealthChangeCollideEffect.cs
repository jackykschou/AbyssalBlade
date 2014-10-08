using Assets.Scripts.GameScripts.GameLogic.Health;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.CollideEffect
{
    [RequireComponent(typeof(HealthChanger))]
    public class HealthChangeCollideEffect : GameLogic 
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

        [GameScriptEventAttribute(GameScriptEvent.OnObjectCollideWithCollideTrigger)]
        public void ApplyHealthChange(GameObject target)
        {
            HealthChanger.ApplyHealthChange(target);
        }
    }
}
