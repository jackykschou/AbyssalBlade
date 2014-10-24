using Assets.Scripts.GameScripts.GameLogic.ObjectMotor;
using UnityEngine;

using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [AddComponentMenu("Skill/SkillEffect/CharacterMove")]
    public class MoveCharacter : SkillEffect
    {
        private Vector2 _direction;
        private CharacterMotor _characterMotor;

        protected override void Initialize()
        {
            base.Initialize();
            _characterMotor = transform.root.GetComponentInChildren<CharacterMotor>();
        }

        public override void Activate()
        {
            base.Activate();
            _characterMotor.MoveCharacter(_direction);
            Activated = false;
        }

        public void UpdateMoveDirection(Vector2 direction)
        {
            _direction = direction;
        }
    }
}
