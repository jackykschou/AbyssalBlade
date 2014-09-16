using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.Components.GameValue;
using Assets.Scripts.Utility;
using UnityEngine;
using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.ObjectMotor
{
    [AddComponentMenu("2DObjectMotor/CharacterMotor")]
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterMotor : ObjectMotor2D
    {
        private const float DecelerationRate = 0.95f;

        protected override void Initialize()
        {
            base.Initialize();
            rigidbody2D.isKinematic = false;
            rigidbody2D.fixedAngle = false;
        }

        public void RigidMove(Vector2 direction)
        {
            FacingDirection newDirection = direction.GetFacingDirection();
            if (newDirection != GameView.FacingDirection)
            {
                TriggerGameScriptEvent(GameScriptEvent.UpdateFacingDirection, direction.GetFacingDirection());
            }
            Debug.Log(Speed.Value);
            rigidbody2D.AddForce(direction * Speed * Time.deltaTime);
        }

        public void RigidMove(GameObject target)
        {
            RigidMove(gameObject.GetDirection(target));
        }

        protected override void Update()
        {
            base.Update();
            rigidbody2D.velocity *= DecelerationRate * Time.deltaTime;
        }
    }
}
