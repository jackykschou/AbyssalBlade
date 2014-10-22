using Assets.Scripts.Attributes;
using Assets.Scripts.GameScripts.GameLogic.PhysicsBody;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.TargetFinders
{
    [AddComponentMenu("TargetFinder/OnCollisionStayFinder")]
    [RequireComponent(typeof(PhysicsBody2D))]
    public class OnCollisionStayFinder : TargetFinder 
    {
        protected override void Deinitialize()
        {
        }

        protected override void FindTargets()
        {
        }

        [GameScriptEvent(Constants.GameScriptEvent.OnPhysicsBodyOnCollisionStay2D)]
        public void OnPhysicsBodyOnCollisionStay2D(Collision2D coll)
        {
            ClearTargets();
            AddTarget(coll.gameObject);
            ApplyEffects();
        }
    }
}
