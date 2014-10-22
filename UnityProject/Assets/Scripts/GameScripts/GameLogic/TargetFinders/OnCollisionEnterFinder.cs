using Assets.Scripts.Attributes;
using Assets.Scripts.GameScripts.GameLogic.PhysicsBody;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.TargetFinders
{
    [AddComponentMenu("TargetFinder/OnCollisionEnterFinder")]
    [RequireComponent(typeof(PhysicsBody2D))]
    public class OnCollisionEnterFinder : TargetFinder 
    {
        protected override void Deinitialize()
        {
        }

        protected override void FindTargets()
        {
        }

        [GameScriptEvent(Constants.GameScriptEvent.OnPhysicsBodyOnCollisionEnter2D)]
        public void OnPhysicsBodyOnCollisionEnter2D(Collision2D coll)
        {
            ClearTargets();
            AddTarget(coll.gameObject);
            ApplyEffects();
        }
    }
}
