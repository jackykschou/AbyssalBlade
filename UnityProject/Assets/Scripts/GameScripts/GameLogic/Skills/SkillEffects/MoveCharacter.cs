using UnityEngine;

using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [AddComponentMenu("Skill/SkillEffect/MoveCharacter")]
    public class MoveCharacter : SkillEffect
    {
        private Vector2 _direction;

        public override void Activate()
        {
            base.Activate();
            SKill.Caster.TriggerGameScriptEvent(GameScriptEvent.MoveCharacter, _direction);
            Activated = false;
        }

        [GameScriptEventAttribute(GameScriptEvent.UpdatePlayerAxis)]
        void UpdateMoveDirection(Vector2 direction)
        {
            _direction = direction;
        }
    }
}
