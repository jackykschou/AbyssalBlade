using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.ObjectMotor.Projectile
{
    [AddComponentMenu("2DObjectMotor/ProjectileMotor/LinearProjectileMotor")]
    public class LinearProjectileMotor : ProjectileMotor
    {
        public EaseType easeType;

        public override void Shoot()
        {
            MoveAlongWithStyle(easeType, MathUtility.GetDirection(transform.position, Target.position), Speed);
        }
    }
}
