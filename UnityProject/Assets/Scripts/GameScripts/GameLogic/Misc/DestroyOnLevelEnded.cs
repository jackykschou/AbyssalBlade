using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Misc
{
    public class DestroyOnLevelEnded : GameLogic
    {
        protected override void Deinitialize()
        {
        }

        [GameEventAttribute(GameEvent.OnLevelEnded)]
        public void DisableGameObjectOnLevelEnded()
        {
            DisableGameObject();
        }
    }
}
