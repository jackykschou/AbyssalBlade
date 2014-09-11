using Assets.Scripts.Utility;
using Assets.Scripts.Utility.GameValue;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.ObjectMotor
{
    using GameLogicEvent = Assets.Scripts.Constants.GameLogicEvent;
    using GameLogicEventAttribute = Assets.Scripts.Attributes.GameLogicEvent;
    using GameEvent = Assets.Scripts.Constants.GameEvent;
    using GameEventtAttribute = Assets.Scripts.Attributes.GameEvent;

    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterMotor : ObjectMotor2D
    {
        private const float DecelerationRate = 0.95f;

        [SerializeField]
        private float _initialSpeed;

        public GameValue Speed;

        protected override void Initialize()
        {
            base.Initialize();
            rigidbody2D.isKinematic = false;
            rigidbody2D.fixedAngle = false;
            Speed = new GameValue(_initialSpeed);
        }

        public void RigidMove(Vector2 direction)
        {
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
