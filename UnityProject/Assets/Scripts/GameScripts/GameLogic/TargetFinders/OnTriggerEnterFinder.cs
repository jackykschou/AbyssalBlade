using Assets.Scripts.Attributes;
using Assets.Scripts.GameScripts.GameLogic.PhysicsBody;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.TargetFinders
{
    [AddComponentMenu("TargetFinder/OnTriggerEnterFinder")]
    [RequireComponent(typeof(PhysicsBody2D))]
    public class OnTriggerEnterFinder : TargetFinder
    {
        protected override void FindTargets()
        {
        }

        protected override void Deinitialize()
        {
        }

        [GameScriptEvent(Constants.GameScriptEvent.OnPhysicsBodyOnTriggerStay2D)]
        public void OnPhysicsBodyOnTriggerStay2D(Collider2D coll)
        {
            ClearTargets();
            AddTarget(coll.gameObject);
            ApplyEffects();
        }
    }
}
