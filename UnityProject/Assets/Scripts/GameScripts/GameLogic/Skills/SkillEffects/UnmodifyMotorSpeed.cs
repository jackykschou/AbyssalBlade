using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.GameValue;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [RequireComponent(typeof(GameValueChanger))]
    [AddComponentMenu("Skill/SkillEffect/UnmodifyMotorSpeed")]
    public class UnmodifyMotorSpeed : SkillEffect
    {
        public GameValueChanger SpeedChanger;

        public override void Activate()
        {
            base.Activate();
            Unmodify();
            Activated = false;
        }

        public void Unmodify()
        {
            Skill.Caster.TriggerGameScriptEvent(GameScriptEvent.UnchangeObjectMotorSpeed, SpeedChanger);
        }
    }
}
