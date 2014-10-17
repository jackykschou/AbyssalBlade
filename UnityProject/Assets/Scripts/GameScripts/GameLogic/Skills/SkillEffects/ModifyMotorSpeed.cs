using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.GameValueTemporaryModifiers;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [AddComponentMenu("Skill/SkillEffect/ModifyMotorSpeed")]
    public class ModifyMotorSpeed : SkillEffect
    {
        public GameValueTemporaryModifier Modifier;

        public override void Activate()
        {
            base.Activate();
            Skill.Caster.TriggerGameScriptEvent(GameScriptEvent.TempChangeObjectMotorSpeed, Modifier);
            Activated = false;
        }
    }
}
