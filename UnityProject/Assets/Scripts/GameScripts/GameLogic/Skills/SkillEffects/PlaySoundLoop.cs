using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;
using Assets.Scripts.Managers;
using Assets.Scripts.Constants;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [AddComponentMenu("Skill/SkillEffect/PlaySoundLoop")]
    public class PlaySoundLoop : SkillEffect
    {
        public LoopName loop;

        [Range(0.0f, 1.0f)]
        public float volume = 5.0f;

        public override void Activate()
        {
            base.Activate();
            Activated = false;
        }

        public void PlaySound()
        {
            AudioManager.Instance.playLoop(loop,volume);
        }

        public void StopSound()
        {
            AudioManager.Instance.stopLoop(loop);
        }
    }
}
