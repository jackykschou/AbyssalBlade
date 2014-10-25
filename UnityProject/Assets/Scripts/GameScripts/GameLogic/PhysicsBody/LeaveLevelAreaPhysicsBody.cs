using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.PhysicsBody
{
    [AddComponentMenu("PhysicsBody/Projectile/LeaveLevelAreaPhysicsBody")]
    public class LeaveLevelAreaPhysicsBody : PhysicsBody2D
    {
        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            gameObject.layer = LayerMask.NameToLayer(LayerConstants.LayerNames.LeaveLevelArea);
            Collider.isTrigger = true;
            Rigidbody.isKinematic = true;
        }
    }
}
