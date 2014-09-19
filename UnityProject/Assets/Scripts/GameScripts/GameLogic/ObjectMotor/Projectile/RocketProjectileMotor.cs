using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.ObjectMotor.Projectile
{
    [AddComponentMenu("2DObjectMotor/ProjectileMotor/RocketProjectileMotor")]
    public class RocketProjectileMotor : ProjectileMotor 
    {
        public override void Shoot()
        {
            MoveAlongWithStyle(EaseType.easeInOutQuart, MathUtility.GetDirection(transform.position, Target));
        }
    }
}
