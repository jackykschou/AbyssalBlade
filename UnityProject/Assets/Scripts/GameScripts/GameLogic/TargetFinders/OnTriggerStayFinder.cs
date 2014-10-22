using Assets.Scripts.Attributes;
using Assets.Scripts.GameScripts.GameLogic.PhysicsBody;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.TargetFinders
{
    [AddComponentMenu("TargetFinder/OnTriggerStayFinder")]
    [RequireComponent(typeof(PhysicsBody2D))]
    public class OnTriggerStayFinder : TargetFinder
    {
        protected override void Deinitialize()
        {
        }

        protected override void FindTargets()
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
