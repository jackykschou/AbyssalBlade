using Assets.Scripts.Utility.GameValue;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic
{
    using GameLogicEvent = Assets.Scripts.Constants.GameLogicEvent;
    using GameLogicEventAttribute = Assets.Scripts.Attributes.GameLogicEvent;
    using GameEvent = Assets.Scripts.Constants.GameEvent;
    using GameEventtAttribute = Assets.Scripts.Attributes.GameEvent;

    public class PlayerCharacterMotor : ObjectMotor2D
    {
        private float _initialSpeed;

        public GameValue Speed;

        protected override void Initialize()
        {
            base.Initialize();
            Speed = new GameValue(_initialSpeed);
        }

        [GameLogicEvent(GameLogicEvent.AxisMoved)]
        void MovePlayer(Vector2 direction)
        {
            MoveTowardsSmooth(direction, Speed);
        }
    }
}
