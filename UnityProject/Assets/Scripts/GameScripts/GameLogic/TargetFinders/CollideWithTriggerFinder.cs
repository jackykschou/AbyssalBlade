using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.TargetFinders
{
    [AddComponentMenu("Skill/SkillEffect/TargetFinder/CollideWithTriggerFinder")]
    public class CollideWithTriggerFinder : TargetFinder
    {
        protected override void FindTargets()
        {
        }

        protected override void Deinitialize()
        {
        }

        protected override void OnTriggerEnter2D(Collider2D coll)
        {
            base.OnTriggerEnter2D(coll);
            ClearTargets();
            AddTarget(coll.gameObject);
            ApplyEffects();
        }

        protected override void OnTriggerStay2D(Collider2D coll)
        {
            base.OnTriggerStay2D(coll);
            ClearTargets();
            AddTarget(coll.gameObject);
            ApplyEffects();
        }
    }
}
