using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills
{
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
    }
}
