using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.ObjectMotor.Projectile
{
    public abstract class ProjectileMotor : ObjectMotor2D
    {
        public Vector3 Target;

        public abstract void Shoot();
    }
}
