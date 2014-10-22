using Assets.Scripts.Attributes;
using Assets.Scripts.GameScripts.GameLogic.PhysicsBody;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.TargetFinders
{
    [AddComponentMenu("TargetFinder/OnTriggerExitFinder")]
    [RequireComponent(typeof(PhysicsBody2D))]
    public class OnTriggerExitFinder : TargetFinder 
    {
        protected override void Deinitialize()
        {
        }

        protected override void FindTargets()
        {
        }

        [GameScriptEvent(Constants.GameScriptEvent.OnPhysicsBodyOnTriggerExit2D)]
        public void OnPhysicsBodyOnTriggerExit2D(Collider2D coll)
        {
            ClearTargets();
            AddTarget(coll.gameObject);
            ApplyEffects();
        }
    }
}
