using Assets.Scripts.GameScripts.Components.DamageApplier;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Damager
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(DamageApplier))]
    [AddComponentMenu("Skill/Damager/CollideDamager/OneTimeSingleTargetCollideDamager")]
    public class OneTimeSingleTargetCollideDamager : GameLogic
    {
        public DamageApplier DamagerApplier;

        protected override void Initialize()
        {
            base.Initialize();
            if (DamagerApplier == null)
            {
                DamagerApplier = GetComponent<DamageApplier>();
            }
        }

        protected override void Deinitialize()
        {
        }

        protected override void OnTriggerEnter2D(Collider2D coll)
        {
            base.OnTriggerEnter2D(coll);

            if (DamagerApplier.ApplyDamage(coll.gameObject))
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
