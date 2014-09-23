﻿using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.PhysicsBody
{
    [AddComponentMenu("PhysicsBody/Projectile/ProjectilePhysicsBody")]
    public class ProjectilePhysicsBody : PhysicsBody2D
    {
        protected override void Initialize()
        {
            base.Initialize();
            Collider.isTrigger = true;
            Rigidbody.isKinematic = false;
            gameObject.layer = LayerMask.NameToLayer(LayerConstants.LayerNames.Projectile);
        }
    }
}
