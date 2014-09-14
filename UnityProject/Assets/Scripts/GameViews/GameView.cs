using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts;
using Assets.Scripts.Utility;
using StateMachine.Action.Math;
using UnityEngine;
using GameLogicEvent = Assets.Scripts.Constants.GameLogicEvent;
using GameLogicEventAttribute = Assets.Scripts.Attributes.GameLogicEvent;

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

        protected abstract Vector2 CenterPosition { get; }

        protected Vector2 ForwardDirection 
        {
            get { return MathUtility.GetFacingDirectionVector(FacingDirection); } 
        }
        protected Vector2 BackwardDirection 
        {
            get { return -MathUtility.GetFacingDirectionVector(FacingDirection); }
        }
        protected Vector2 LeftwardDirection 
        {
            get
            {
                Vector2 dir = MathUtility.GetFacingDirectionVector(FacingDirection);
                return Quaternion.AngleAxis(-90f, Vector3.forward) * new Vector3(dir.x, dir.y, 0);
            }
        }

        protected Vector2 RightwardDirection
        {
            get
            {
                Vector2 dir = MathUtility.GetFacingDirectionVector(FacingDirection);
                return Quaternion.AngleAxis(90f, Vector3.forward) * new Vector3(dir.x, dir.y, 0);
            }
        }

        protected abstract Vector2 ForwardEdge { get; }
        protected abstract Vector2 BackwardEdge { get; }
        protected abstract Vector2 LeftwardEdge { get; }
        protected abstract Vector2 RightwardEdge { get; }

        [GameLogicEventAttribute(GameLogicEvent.UpdateFacingDirection)]
        public void UpdateFacingDirection(FacingDirection facingDirection)
        {
            FacingDirection = facingDirection;
        }
    }
}
