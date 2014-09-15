﻿using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.PhysicsBody
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class PhysicsBody2D : GameLogic
    {
        protected Collider2D Collider;
        protected Rigidbody2D Rigidbody;
        protected override void Initialize()
        {
            Collider = GetComponent<Collider2D>();
            Rigidbody = GetComponent<Rigidbody2D>();
        }

        protected override void Deinitialize()
        {
        }
    }
}
