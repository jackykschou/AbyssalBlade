﻿using Assets.Scripts.Utility.GameValue;
using UnityEngine;

using GameLogicEvent = Assets.Scripts.Constants.GameLogicEvent;
using GameLogicEventAttribute = Assets.Scripts.Attributes.GameLogicEvent;

namespace Assets.Scripts.GameScripts.GameLogic
{
    public class ObjectMotor2D : GameLogic
    {
        public float Speed;

        private GameValue _speedGameValue;

        private float _velocityX;
        private float _velocityY;

        protected override void Initialize()
        {
            _speedGameValue = new GameValue(Speed);
            _velocityX = 0f;
            _velocityY = 0f;
        }

        protected override void Deinitialize()
        {
        }

        public void MoveTowardsLinear(Vector2 direction)
        {
            transform.Translate(new Vector3(direction.x, direction.y, 0) * _speedGameValue.Value * Time.deltaTime);
        }

        public void MoveTowardsSmooth(Vector2 direction)
        {
            const float smoothDampSmoothness = 5.0f;

            Vector3 destination = transform.position + new Vector3(direction.x, direction.y, 0) * Speed * Time.deltaTime;

            float posX = Mathf.SmoothDamp(transform.position.x, destination.x, ref _velocityX, Time.deltaTime * smoothDampSmoothness);
            float posY = Mathf.SmoothDamp(transform.position.y, destination.y, ref _velocityY, Time.deltaTime * smoothDampSmoothness);

            transform.position = new Vector3(posX, posY, transform.position.z);
        } 

        public void TeleportTo(Vector2 position)
        {
            transform.position = new Vector3(position.x, position.y, transform.position.z);
        }

        [GameLogicEventAttribute(GameLogicEvent.AxisMoved)]
        public void GameLogicEventPublic(Vector2 direction)
        {
            MoveTowardsSmooth(direction);
        }
    }
}
