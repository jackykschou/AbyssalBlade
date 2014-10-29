using Assets.Scripts.Constants;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.ObjectMotor.Projectile
{
    [AddComponentMenu("2DObjectMotor/ProjectileMotor/MoveToLocationProjectileMotor")]
    public class MoveToLocationProjectileMotor : ProjectileMotor
    {
        public EaseType EaseType;

        private Vector2 _destinaion;
        private bool _arrived = false;

        protected override void Initialize()
        {
            base.Initialize();
            _arrived = false;
        }

        public override void Shoot()
        {
            _destinaion = Target.position;
            TriggerGameScriptEvent(GameScriptEvent.UpdateFacingDirection, UtilityFunctions.GetDirection(transform.position, Target.position).GetFacingDirection());
            MoveToWithStyle(EaseType, Target.transform.position, Speed);
        }

        protected override void Update()
        {
            base.Update();
            if (!_arrived && ((Vector2)transform.position) == _destinaion)
            {
                _arrived = true;
                TriggerGameScriptEvent(GameScriptEvent.OnProjectileArriveDestination, _destinaion);
            }
        }
    }
}
