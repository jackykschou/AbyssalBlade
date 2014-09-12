using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts;
using UnityEngine;
using GameLogicEvent = Assets.Scripts.Constants.GameLogicEvent;
using GameLogicEventAttribute = Assets.Scripts.Attributes.GameLogicEvent;

namespace Assets.Scripts.GameViews
{
    [AddComponentMenu("GameView/GameView")]
    public abstract class GameView : GameScript
    {
        public FacingDirection FacingDirection { get; set; }

        protected override void Initialize()
        {
            FacingDirection = FacingDirection.Down;
        }

        protected abstract override void Deinitialize();

        [GameLogicEventAttribute(GameLogicEvent.UpdateFacingDirection)]
        public void UpdateFacingDirection(FacingDirection facingDirection)
        {
            FacingDirection = facingDirection;
        }
    }
}
