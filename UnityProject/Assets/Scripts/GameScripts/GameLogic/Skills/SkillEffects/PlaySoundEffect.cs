using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Utility;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;
using Assets.Scripts.Managers;
using Assets.Scripts.Constants;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [AddComponentMenu("Skill/SkillEffect/PlaySoundEffect")]
    public class PlaySoundEffect : SkillEffect
    {
        public ClipName clip;

        public override void EditorUpdate()
        {
        }

        public override void Activate()
        {
            base.Activate();
            Activated = false;
        }

        public void PlaySound()
        {
            AudioManager.Instance.playClip(clip);
        }
    }
}
