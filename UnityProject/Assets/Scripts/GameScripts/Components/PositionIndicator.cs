using System;
using Assets.Scripts.Constants;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.Components
{
    [Serializable]
    public class PositionIndicator : GameScriptComponent
    {
        public Transform Follower;
        public Transform Position;

        private Vector3 _downPos;
        private Vector3 _upPos;
        private Vector3 _leftPos;
        private Vector3 _rightPos;

        public override void Initialize()
        {
            _downPos = Position.position;
            Position.RotateAround (Follower.position, Vector3.forward, 90);
            _rightPos = Position.position;
            Position.RotateAround(Follower.position, Vector3.forward, 90);
            _upPos = Position.position;
            Position.RotateAround(Follower.position, Vector3.forward, 90);
            _leftPos = Position.position;

            Position.position = _downPos;
        }

        public override void Deinitialize()
        {
        }

        public override void Update () 
        {
        }

        public void UpdatePosition(FacingDirection facingDirection)
        {
            switch (facingDirection)
            {
                case FacingDirection.Up:
                    Position.position = _upPos;
                    break;
                case FacingDirection.Down:
                    Position.position = _downPos;
                    break;
                case FacingDirection.Left:
                    Position.position = _leftPos;
                    break;
                case FacingDirection.Right:
                    Position.position = _rightPos;
                    break;
            }
        }
    }
}
