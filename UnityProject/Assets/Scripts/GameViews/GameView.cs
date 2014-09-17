﻿using Assets.Scripts.Attributes;
using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts;
using Assets.Scripts.Utility;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;

namespace Assets.Scripts.GameViews
{
    [AddComponentMenu("GameView/GameView")]
    public abstract class GameView : GameScript
    {
        public FacingDirection FacingDirection { get; private set; }

        protected override void Initialize()
        {
            FacingDirection = FacingDirection.Down;
        }

        protected abstract override void Deinitialize();

        public abstract Vector2 CenterPosition { get; }

        public Vector2 ForwardDirection 
        {
            get { return MathUtility.GetFacingDirectionVector(FacingDirection); } 
        }
        public Vector2 BackwardDirection 
        {
            get { return -MathUtility.GetFacingDirectionVector(FacingDirection); }
        }
        public Vector2 LeftwardDirection 
        {
            get
            {
                Vector2 dir = MathUtility.GetFacingDirectionVector(FacingDirection);
                return Quaternion.AngleAxis(-90f, Vector3.forward) * new Vector3(dir.x, dir.y, 0);
            }
        }

        public Vector2 RightwardDirection
        {
            get
            {
                Vector2 dir = MathUtility.GetFacingDirectionVector(FacingDirection);
                return Quaternion.AngleAxis(90f, Vector3.forward) * new Vector3(dir.x, dir.y, 0);
            }
        }

        public abstract Vector2 ForwardEdge { get; }
        public abstract Vector2 BackwardEdge { get; }
        public abstract Vector2 LeftwardEdge { get; }
        public abstract Vector2 RightwardEdge { get; }

        [Attributes.GameScriptEvent(GameScriptEvent.UpdateFacingDirection)]
        public void UpdateFacingDirection(FacingDirection facingDirection)
        {
            FacingDirection = facingDirection;
        }
    }
}
