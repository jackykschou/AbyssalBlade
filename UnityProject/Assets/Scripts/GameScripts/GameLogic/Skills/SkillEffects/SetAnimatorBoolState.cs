using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [AddComponentMenu("Skill/SkillEffect/SetAnimatorBoolState")]
    public class SetAnimatorBoolState : SkillEffect
    {
        public string StateName;

        [GameScriptEventAttribute(GameScriptEvent.SkillCastTriggerSucceed)]
        public void SetAnimatorState()
        {
            TriggerCasterGameScriptEvent(GameScriptEvent.SetAnimatorState, StateName);
        }
    }
}
