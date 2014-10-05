using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.Components;
using Assets.Scripts.GameScripts.GameLogic.Health;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(HealthChanger))]
    [AddComponentMenu("Skill/Damager/CollideDamager/MultipleTargetCollideHealthChanger")]
    public class MultipleTargetCollideHealthChanger : GameLogic
    {
        public HealthChanger HealthChanger;
        public Collider2D Collider;

        protected override void Initialize()
        {
            base.Initialize();
            if (HealthChanger == null)
            {
                HealthChanger = GetComponent<HealthChanger>();
            }
            Collider = GetComponent<Collider2D>();
            Collider.enabled = true;
        }

        protected override void Deinitialize()
        {
        }

        protected override void OnTriggerEnter2D(Collider2D coll)
        {
            base.OnTriggerEnter2D(coll);

            HealthChanger.ApplyHealthChange(coll.gameObject);
        }
    }
}
