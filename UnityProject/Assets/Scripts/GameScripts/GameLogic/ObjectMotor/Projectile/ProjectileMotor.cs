using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.ObjectMotor.Projectile
{
    public abstract class ProjectileMotor : ObjectMotor2D
    {
        public Transform Target;

        public abstract void Shoot();
        public abstract void Shoot(Vector3 direction);
    }
}
