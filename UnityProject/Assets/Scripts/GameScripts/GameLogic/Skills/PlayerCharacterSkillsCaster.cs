using UnityEngine;

using GameLogicEvent = Assets.Scripts.Constants.GameLogicEvent;
using GameLogicEventAttribute = Assets.Scripts.Attributes.GameLogicEvent;

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

        [GameLogicEventAttribute(GameLogicEvent.PlayerAttack1ButtonPressed)]
        public void ActivateSkillOne()
        {
            _skill1.Activate();
        }

        [GameLogicEventAttribute(GameLogicEvent.PlayerAttack2ButtonPressed)]
        public void ActivateSkillTwo()
        {
            _skill2.Activate();
        }

        [GameLogicEventAttribute(GameLogicEvent.PlayerAttack3ButtonPressed)]
        public void ActivateSkillThree()
        {
            _skill3.Activate();
        }

        [GameLogicEventAttribute(GameLogicEvent.PlayerAttack4ButtonPressed)]
        public void ActivateSkillFour()
        {
            _skill4.Activate();
        }
    }
}
