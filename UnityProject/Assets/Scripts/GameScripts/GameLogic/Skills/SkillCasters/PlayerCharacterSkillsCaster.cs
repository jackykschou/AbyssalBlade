using Assets.Scripts.GameScripts.GameLogic.Misc;
using Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects;
using Assets.Scripts.Utility;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillCasters
{
    [AddComponentMenu("Skill/Caster/PlayerCharacterCaster")]
    [RequireComponent(typeof(PositionIndicator))]
    public class PlayerCharacterSkillsCaster : SkillCaster
    {
        public Skill MovementSkill;

        public Skill Skill1;

        public Skill Skill2;

        public Skill Skill3;

        public Skill Skill4;

        public Skill Dash;

        public PositionIndicator TargetHolder;

        private MoveCharacter _moveCharacter;

        [HideInInspector]
        public Vector2 Direction;

        public override Vector2 PointingDirection
        {
            get { return TargetHolder.Direction; }
        }

        protected override void Initialize()
        {
            base.Initialize();
            Target = TargetHolder.Position;
            _moveCharacter = GetComponentInChildren<MoveCharacter>();
        }

        protected override void Deinitialize()
        {
        }

        public void ActivateMovement(Vector2 direction)
        {
            _moveCharacter.UpdateMoveDirection(direction);
            MovementSkill.Activate();
        }

        public void ActivateSkillOne()
        {
            if (Skill1.CanActivate())
            {
                UpdateFacingDirection();
            }
            Skill1.Activate();
        }

        public void ActivateSkillTwo()
        {
            if (Skill2.CanActivate())
            {
                UpdateFacingDirection();
            }
            Skill2.Activate();
        }

        public void ActivateSkillThree()
        {
            if (Skill3.CanActivate())
            {
                UpdateFacingDirection();
            }
            Skill3.Activate();
        }

        public void ActivateSkillFour()
        {
            if (Skill4.CanActivate())
            {
                UpdateFacingDirection();
            }
            Skill4.Activate();
        }

        public void ActivateDash()
        {
            if (Dash.CanActivate())
            {
                UpdateFacingDirection();
            }
            Dash.Activate();
        }

        private void UpdateFacingDirection()
        {
            TriggerGameScriptEvent(GameScriptEvent.UpdateFacingDirection,  Direction.GetFacingDirection());
        }
    }
}
