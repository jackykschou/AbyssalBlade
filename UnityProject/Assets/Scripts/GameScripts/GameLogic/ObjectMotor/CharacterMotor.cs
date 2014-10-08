using Assets.Scripts.Constants;
using Assets.Scripts.Utility;
using UnityEngine;
using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

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
            rigidbody2D.fixedAngle = true;
        }

        [GameScriptEvent(GameScriptEvent.CharacterMove)]
        public void MoveCharacter(Vector2 direction)
        {
            direction = direction.normalized;
            FacingDirection newDirection = direction.GetFacingDirection();
            if (newDirection != GameView.FacingDirection)
            {
                TriggerGameScriptEvent(GameScriptEvent.UpdateFacingDirection, newDirection);
            }
            TriggerGameScriptEvent(GameScriptEvent.OnCharacterMove, direction);
            RigidMove(direction, Speed);
        }

        [GameScriptEvent(GameScriptEvent.OnCharacterKnockBacked)]
        public void RigidMove(Vector2 direction, float speed)
        {
            direction = direction.normalized;
            rigidbody2D.AddForce(direction * speed * WorldScaleConstant.SpeedScale * Time.fixedDeltaTime);
            TriggerGameScriptEvent(GameScriptEvent.OnObjectMove);
        }

        public void RigidMove(GameObject target, float speed)
        {
            RigidMove(gameObject.GetDirection(target), speed);
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            rigidbody2D.velocity *= DecelerationRate * Time.fixedDeltaTime;
        }
    }
}
