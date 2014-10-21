using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.GameValue;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [RequireComponent(typeof(GameValueChanger))]
    [AddComponentMenu("Skill/SkillEffect/ModifyMotorSpeed")]
    public class ModifyMotorSpeed : SkillEffect
    {
        public GameValueChanger SpeedChanger;

        public override void Activate()
        {
            base.Activate();
            Modify();
            Activated = false;
        }

        public void Modify()
        {
            Skill.Caster.TriggerGameScriptEvent(GameScriptEvent.ChangeObjectMotorSpeed, SpeedChanger);
        }
    }
}
