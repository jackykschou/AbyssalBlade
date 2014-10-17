using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.GameValueTemporaryModifiers;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [AddComponentMenu("Skill/SkillEffect/UnmodifyMotorSpeed")]
    public class UnmodifyMotorSpeed : SkillEffect 
    {
        public GameValueTemporaryModifier Modifier;

        public override void Activate()
        {
            base.Activate();
            Skill.Caster.TriggerGameScriptEvent(GameScriptEvent.UnchangeObjectMotorSpeed, Modifier);
            Activated = false;
        }
    }
}
