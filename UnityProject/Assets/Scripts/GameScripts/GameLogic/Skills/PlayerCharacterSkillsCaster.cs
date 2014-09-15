using Assets.Scripts.Attributes;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills
{
    [AddComponentMenu("Skill/Caster/PlayerCharacterCaster")]
    public class PlayerCharacterSkillsCaster : GameLogic
    {
        [SerializeField] 
        private Skill _skill1;

        [SerializeField]
        private Skill _skill2;

        [SerializeField]
        private Skill _skill3;

        [SerializeField]
        private Skill _skill4;

        protected override void Deinitialize()
        {
        }

        [GameScriptEvent(Constants.GameScriptEvent.PlayerAttack1ButtonPressed)]
        public void ActivateSkillOne()
        {
            _skill1.Activate();
        }

        [GameScriptEvent(Constants.GameScriptEvent.PlayerAttack2ButtonPressed)]
        public void ActivateSkillTwo()
        {
            _skill2.Activate();
        }

        [GameScriptEvent(Constants.GameScriptEvent.PlayerAttack3ButtonPressed)]
        public void ActivateSkillThree()
        {
            _skill3.Activate();
        }

        [GameScriptEvent(Constants.GameScriptEvent.PlayerAttack4ButtonPressed)]
        public void ActivateSkillFour()
        {
            _skill4.Activate();
        }
    }
}
