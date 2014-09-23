using Assets.Scripts.Attributes;
using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.Components;
using UnityEngine;

using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillCasters
{
    [AddComponentMenu("Skill/Caster/PlayerCharacterCaster")]
    public class PlayerCharacterSkillsCaster : SkillCaster
    {
        [SerializeField] 
        private Skill _movementSkill;

        [SerializeField] 
        private Skill _skill1;

        [SerializeField]
        private Skill _skill2;

        [SerializeField]
        private Skill _skill3;

        [SerializeField]
        private Skill _skill4;

        public PositionIndicator TargetHolder;

        protected override void Initialize()
        {
            base.Initialize();
            Target = TargetHolder.Position;
        }

        protected override void Deinitialize()
        {
        }

        [GameScriptEventAttribute(GameScriptEvent.UpdateFacingDirection)]
        public void UpdateDamageAreaPosition(FacingDirection facingDirection)
        {
            TargetHolder.UpdatePosition(facingDirection);
        }

        [GameScriptEventAttribute(GameScriptEvent.PlayerAxisMoved)]
        public void ActivateMovement(Vector2 direction)
        {
            TriggerGameScriptEvent(GameScriptEvent.UpdatePlayerAxis, direction);
            _movementSkill.Activate();
        }

        [GameScriptEvent(GameScriptEvent.PlayerAttack1ButtonPressed)]
        public void ActivateSkillOne()
        {
            _skill1.Activate();
        }

        [GameScriptEvent(GameScriptEvent.PlayerAttack2ButtonPressed)]
        public void ActivateSkillTwo()
        {
            _skill2.Activate();
        }

        [GameScriptEvent(GameScriptEvent.PlayerAttack3ButtonPressed)]
        public void ActivateSkillThree()
        {
            _skill3.Activate();
        }

        [GameScriptEvent(GameScriptEvent.PlayerAttack4ButtonPressed)]
        public void ActivateSkillFour()
        {
            _skill4.Activate();
        }
    }
}
