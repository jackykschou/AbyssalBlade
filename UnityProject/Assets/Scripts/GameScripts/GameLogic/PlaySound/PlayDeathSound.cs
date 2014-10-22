using Assets.Scripts.Constants;
using Assets.Scripts.Managers;
using UnityEngine;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.PlaySound
{
    public class PlayDeathSound : GameLogic 
    {
        public ClipName clip;

        [Range(0.0f, 1.0f)]
        public float volume = 1.0f;

        [GameScriptEventAttribute(GameScriptEvent.OnObjectHasNoHitPoint)]
        public void StartPlayDeathSound()
        {
            AudioManager.Instance.PlayClip(clip, gameObject, volume);
        }

        protected override void Deinitialize()
        {
        }
    }
}
