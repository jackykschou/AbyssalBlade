using Assets.Scripts.Managers;
using UnityEngine;
using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Misc
{
    public class OnPlayerDeathReset : GameLogic
    {
        [GameEventAttribute(GameEvent.OnLevelEnded)]
        public void ShowDeathMessage()
        {
            if (LevelManager.Instance.IsPlayLevel)
            {
                MessageManager.Instance.DisplayMessage("You have Died.", Vector2.up);
                AudioManager.Instance.Mute();
                
            }
        }
        [GameEventAttribute(GameEvent.OnLevelStarted)]
        public void LevelStart()
        {
            if (LevelManager.Instance.IsPlayLevel)
            {
                AudioManager.Instance.UnMute();

            }
        }
        // Use this for initialization
        protected override void Deinitialize()
        {
        }
    }
}