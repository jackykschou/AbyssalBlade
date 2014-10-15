using Assets.Scripts.Attributes;
using Assets.Scripts.GameScripts.Components.TimeDispatcher;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.CastableCondition
{
    [AddComponentMenu("Skill/CastableCondition/TimeCoolDown")]
    public class SkillCooldown : SkillCastableCondition
    {
        public FixTimeDispatcher CoolDownDispatcher;

        protected override void Deinitialize()
        {
        }

        protected override void Update()
        {
            base.Update();
            UpdateSkillCooldown();
        }

        public override bool CanCast()
        {
            return CoolDownDispatcher.CanDispatch();
        }

        public void UpdateSkillCooldown()
        {
            TriggerGameScriptEvent(Constants.GameScriptEvent.UpdateSkillCooldownPercentage, Skill, CoolDownDispatcher.DispatchCoolDownPercentage);
        }

        [GameScriptEvent(Constants.GameScriptEvent.SkillCastFinished)]
        public void ResetCooldown(Skill skill)
        {
            if (skill == Skill)
            {
                CoolDownDispatcher.Dispatch();
            }
        }
    }
}
