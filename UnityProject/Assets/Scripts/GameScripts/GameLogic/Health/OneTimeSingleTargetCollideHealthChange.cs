using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Health
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(HealthChanger))]
    [AddComponentMenu("Skill/Damager/CollideDamager/OneTimeSingleTargetCollideHealthChange")]
    public class OneTimeSingleTargetCollideHealthChange : GameLogic
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

        protected override void OnTriggerEnter2D(Collider2D coll)
        {
            base.OnTriggerEnter2D(coll);

            if (HealthChanger.ApplyHealthChange(coll.gameObject))
            {
                DisableGameObject();
                return;;
            }

            if (coll.gameObject.tag != gameObject.tag)
            {
                DisableGameObject();
            }
        }
    }
}
