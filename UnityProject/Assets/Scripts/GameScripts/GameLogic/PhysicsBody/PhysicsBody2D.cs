using UnityEngine;

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
            rigidbody2D.gravityScale = 0f;
            Debug.Log("lool");   
            Rigidbody.fixedAngle = true;
        }

        protected override void Deinitialize()
        {
        }
    }
}
