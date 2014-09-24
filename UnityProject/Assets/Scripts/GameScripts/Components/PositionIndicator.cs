using System;
using Assets.Scripts.Constants;
using Assets.Scripts.GameViews;
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
            if (Follower == null)
            {
                Follower = GameScript.transform;
            }
            if (Position == null)
            {
                throw new Exception("Position is null");
            }

            _downPos = Position.localPosition;
            Position.RotateAround (Follower.position, Vector3.forward, 90);
            _rightPos = Position.localPosition;
            Position.RotateAround(Follower.position, Vector3.forward, 90);
            _upPos = Position.localPosition;
            Position.RotateAround(Follower.position, Vector3.forward, 90);
            _leftPos = Position.localPosition;

            Position.localPosition = _downPos;
        }

        public override void Deinitialize()
        {
        }

        public void UpdatePosition(FacingDirection facingDirection)
        {
            switch (facingDirection)
            {
                case FacingDirection.Up:
                    Position.localPosition = _upPos;
                    break;
                case FacingDirection.Down:
                    Position.localPosition = _downPos;
                    break;
                case FacingDirection.Left:
                    Position.localPosition = _leftPos;
                    break;
                case FacingDirection.Right:
                    Position.localPosition = _rightPos;
                    break;
            }
        }
    }
}
