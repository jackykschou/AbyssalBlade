using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PhysicsBody2D : GameLogic
    {
        protected override void Initialize()
        {
        }

        protected override void Deinitialize()
        {
        }
    }
}
