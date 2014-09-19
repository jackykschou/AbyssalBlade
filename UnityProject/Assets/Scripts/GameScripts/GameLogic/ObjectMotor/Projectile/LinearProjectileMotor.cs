using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.ObjectMotor.Projectile
{
    [AddComponentMenu("2DObjectMotor/ProjectileMotor/LinearProjectileMotor")]
    public class LinearProjectileMotor : ProjectileMotor 
    {
        public override void Shoot()
        {
            MoveAlongWithStyle(EaseType.linear, MathUtility.GetDirection(transform.position, Target));
        }
    }
}
