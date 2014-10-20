using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [AddComponentMenu("Skill/SkillEffect/ModifyMotorSpeed")]
    public class ModifyMotorSpeed : SkillEffect
    {
        public override void Activate()
        {
            base.Activate();
            Activated = false;
        }
    }
}
