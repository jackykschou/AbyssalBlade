using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    public class PushCharacter : SkillEffect 
    {
        private Vector2 _direction;
        [Range(0f, 100f)]
        public float Speed;
        [Range(0f, 10f)]
        public float Time;

        public override void Activate()
        {
            base.Activate();
            Skill.Caster.TriggerGameScriptEvent(GameScriptEvent.PushCharacter, _direction, Speed, Time);
            Activated = false;
        }

        [GameScriptEventAttribute(GameScriptEvent.UpdatePlayerAxis)]
        void UpdateMoveDirection(Vector2 direction)
        {
            _direction = direction;
        }
    }
}
