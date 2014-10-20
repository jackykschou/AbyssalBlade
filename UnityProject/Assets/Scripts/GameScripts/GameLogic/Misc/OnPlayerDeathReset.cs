using Assets.Scripts.Managers;
using UnityEngine;
using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Misc
{
    public class OnPlayerDeathReset : GameLogic
    {
        [GameEventAttribute(GameEvent.OnPlayerDeath)]
        public void ShowDeathMessage()
        {
            MessageManager.Instance.DisplayDeathMessage();
            AudioManager.Instance.Mute();
        }

        [GameEventAttribute(GameEvent.OnLevelStarted)]
        public void UnMuteAudio()
        {
            AudioManager.Instance.UnMute();
        }


        // Use this for deinitialization
        protected override void Deinitialize()
        {
        }
    }
}