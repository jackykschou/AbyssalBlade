using Assets.Scripts.Attributes;
using Assets.Scripts.GameScripts.GameLogic.PhysicsBody;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.TargetFinders
{
    [AddComponentMenu("TargetFinder/OnCollisionExitFinder")]
    [RequireComponent(typeof(PhysicsBody2D))]
    public class OnCollisionExitFinder : TargetFinder
    {
        protected override void Deinitialize()
        {
        }

        protected override void FindTargets()
        {
        }

        [GameScriptEvent(Constants.GameScriptEvent.OnPhysicsBodyOnCollisionExit2D)]
        public void OnPhysicsBodyOnCollisionExit2D(Collision2D coll)
        {
            ClearTargets();
            AddTarget(coll.gameObject);
            ApplyEffects();
        }
    }
}
