using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [AddComponentMenu("Skill/SkillEffect/UnmodifyMotorSpeed")]
    public class UnmodifyMotorSpeed : SkillEffect 
    {
        public override void Activate()
        {
            base.Activate();
            Activated = false;
        }
    }
}
