using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.Components.GameValue;
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

        [GameScriptEvent(GameScriptEvent.PlayerAxisMoved)]
        public void RigidMove(Vector2 direction)
        {
            direction = direction.normalized;
            FacingDirection newDirection = direction.GetFacingDirection();
            if (newDirection != GameView.FacingDirection)
            {
                TriggerGameScriptEvent(GameScriptEvent.UpdateFacingDirection, newDirection);
            }
            rigidbody2D.AddForce(direction * Speed * WorldScaleConstant.SpeedScale * Time.deltaTime);
            TriggerGameScriptEvent(GameScriptEvent.OnObjectMove);
        }

        [GameScriptEvent(GameScriptEvent.AIMove)]
        public void RigidMoveLol(Vector2 direction)
        {
            direction = direction.normalized;
            FacingDirection newDirection = direction.GetFacingDirection();
            if (newDirection != GameView.FacingDirection)
            {
                TriggerGameScriptEvent(GameScriptEvent.UpdateFacingDirection, newDirection);
            }
            rigidbody2D.AddForce(direction * Speed * WorldScaleConstant.SpeedScale * Time.deltaTime);
            TriggerGameScriptEvent(GameScriptEvent.OnObjectMove);
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
