using Assets.Scripts.Constants;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.ObjectMotor.Projectile
{
    [AddComponentMenu("2DObjectMotor/ProjectileMotor/LinearProjectileMotor")]
    public class LinearProjectileMotor : ProjectileMotor
    {
        public EaseType EaseType;

        public override void Shoot()
        {
            Vector2 direction = UtilityFunctions.GetDirection(transform.position, Target.position);
            TriggerGameScriptEvent(GameScriptEvent.UpdateFacingDirection, direction.GetFacingDirection());
            MoveAlongWithStyle(EaseType, direction, Speed, 50f);
        }
        public override void Shoot(Vector3 direction)
        {
            MoveAlongWithStyle(EaseType, direction, Speed, 50f);
        }
    }
}
