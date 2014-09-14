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
            TriggerGameLogicEvent(Constants.GameLogicEvent.UpdateSkillCooldownPercentage, CoolDownDispatcher.DispatchCoolDownPercentage);
        }

        [GameLogicEvent(Constants.GameLogicEvent.SkillCastTriggerSucceed)]
        public void ResetCooldown()
        {
            CoolDownDispatcher.Dispatch();
        }
    }
}
