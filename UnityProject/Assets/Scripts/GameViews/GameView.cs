using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts;
using GameLogicEvent = Assets.Scripts.Constants.GameLogicEvent;
using GameLogicEventAttribute = Assets.Scripts.Attributes.GameLogicEvent;

namespace Assets.Scripts.GameViews
{
    public abstract class GameView : GameScript
    {
        public FacingDirection FacingDirection { get; set; }

        protected abstract override void Initialize();

        protected abstract override void Deinitialize();

        [GameLogicEventAttribute(GameLogicEvent.UpdateGameViewFacingDirection)]
        public void UpdateFacingDirection(FacingDirection facingDirection)
        {
            FacingDirection = facingDirection;
        }
    }
}
