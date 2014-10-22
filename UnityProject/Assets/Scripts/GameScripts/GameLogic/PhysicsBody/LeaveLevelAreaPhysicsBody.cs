using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.PhysicsBody
{
    [AddComponentMenu("PhysicsBody/Projectile/LeaveLevelAreaPhysicsBody")]
    public class LeaveLevelAreaPhysicsBody : PhysicsBody2D
    {
        protected override void Initialize()
        {
            base.Initialize();
            gameObject.layer = LayerConstants.LayerMask.LeaveLevelArea;
            Collider.isTrigger = true;
            Rigidbody.isKinematic = true;
        }
    }
}
