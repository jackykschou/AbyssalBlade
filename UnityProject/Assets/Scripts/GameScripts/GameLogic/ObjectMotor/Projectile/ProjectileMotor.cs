using Assets.Scripts.Attributes;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.ObjectMotor.Projectile
{
    public abstract class ProjectileMotor : ObjectMotor2D
    {
        public Transform Target;
        public Vector2 Direction;
        public Vector2 Destination;

        [GameScriptEvent(Constants.GameScriptEvent.UpdateProjectileDirection)]
        public void UpdateDirection(Vector2 direction)
        {
            Direction = direction;
        }

        [GameScriptEvent(Constants.GameScriptEvent.UpdateProjectileTarget)]
        public void UpdateTarget(Transform target)
        {
            Target = target;
        }

        [GameScriptEvent(Constants.GameScriptEvent.UpdateProjectileDestination)]
        public void UpdateTarget(Vector2 destination)
        {
            Destination = destination;
        }

        [GameScriptEvent(Constants.GameScriptEvent.ShootProjectile)]
        public abstract void Shoot();
    }
}
