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
        public Skill MovementSkill;

        public Skill Skill1;

        public Skill Skill2;

        public Skill Skill3;

        public Skill Skill4;

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
            MovementSkill.Activate();
        }

        [GameScriptEvent(GameScriptEvent.PlayerAttack1ButtonPressed)]
        public void ActivateSkillOne()
        {
            Skill1.Activate();
        }

        [GameScriptEvent(GameScriptEvent.PlayerAttack2ButtonPressed)]
        public void ActivateSkillTwo()
        {
            Skill2.Activate();
        }

        [GameScriptEvent(GameScriptEvent.PlayerAttack3ButtonPressed)]
        public void ActivateSkillThree()
        {
            Skill3.Activate();
        }

        [GameScriptEvent(GameScriptEvent.PlayerAttack4ButtonPressed)]
        public void ActivateSkillFour()
        {
            Skill4.Activate();
        }
    }
}
