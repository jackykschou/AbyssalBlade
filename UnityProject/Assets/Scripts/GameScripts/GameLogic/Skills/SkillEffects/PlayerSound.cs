using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [AddComponentMenu("Skill/SkillEffect/PlayerSound")]
    public class PlayerSound : SkillEffect 
    {
        public override void Activate()
        {
            base.Activate();
            Activated = false;
        }
    }
}
