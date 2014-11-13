using Assets.Scripts.Constants;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.ObjectMotor.Projectile
{
    [AddComponentMenu("2DObjectMotor/ProjectileMotor/MoveToLocationProjectileMotor")]
    public class MoveToLocationProjectileMotor : ProjectileMotor
    {
        public EaseType EaseType;

        private bool _arrived = false;

        protected override void Initialize()
        {
            base.Initialize();
            _arrived = false;
        }

        public override void Shoot()
        {
            TriggerGameScriptEvent(GameScriptEvent.UpdateFacingDirection,
                UtilityFunctions.GetDirection(transform.position, Destination).GetFacingDirection());
            MoveToWithStyle(EaseType, Destination, Speed);
        }


        protected override void Update()
        {
            base.Update();
            if (!_arrived && ((Vector2)transform.position) == Destination)
            {
                _arrived = true;
                TriggerGameScriptEvent(GameScriptEvent.OnProjectileArriveDestination, Destination);
            }
        }
    }
}
