using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.TargetFinders
{
    [AddComponentMenu("TargetFinder/CollideWithTriggerFinder")]
    [RequireComponent(typeof(Collider2D))]
    public class CollideWithTriggerFinder : TargetFinder
    {
        private Collider2D _collider;

        protected override void FindTargets()
        {
        }

        protected override void Initialize()
        {
            base.Initialize();
            _collider = GetComponent<Collider2D>();
            _collider.enabled = true;
            _collider.isTrigger = true;
        }

        protected override void Deinitialize()
        {
        }

        protected override void OnTriggerEnter2D(Collider2D coll)
        {
            ClearTargets();
            AddTarget(coll.gameObject);
            ApplyEffects();
        }

        protected override void OnTriggerStay2D(Collider2D coll)
        {
            ClearTargets();
            AddTarget(coll.gameObject);
            ApplyEffects();
        }
    }
}
