using Assets.Scripts.Attributes;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.CastableCondition
{
    [AddComponentMenu("Skill/CastableCondition/TimeCoolDown")]
    public class SkillCooldown : SkillCastableCondition
    {
        [SerializeField] 
        private float _initializeCoolDown;

        private FixTimeDispatcher _coolDownDispatcher;

        protected override void Initialize()
        {
            base.Initialize();
            _coolDownDispatcher = new FixTimeDispatcher(_initializeCoolDown);
        }

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
            return _coolDownDispatcher.CanDispatch();
        }

        public void UpdateSkillCooldown()
        {
            TriggerGameLogicEvent(Constants.GameLogicEvent.UpdateSkillCooldownPercentage, _coolDownDispatcher.DispatchCoolDownPercentage);
        }

        [GameLogicEvent(Constants.GameLogicEvent.SkillCastTriggerSucceed)]
        public void ResetCooldown()
        {
            _coolDownDispatcher.Dispatch();
        }
    }
}
