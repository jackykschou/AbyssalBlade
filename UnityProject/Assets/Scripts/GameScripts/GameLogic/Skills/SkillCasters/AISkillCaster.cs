using System.Linq;
using Assets.Scripts.GameScripts.Components.TimeDispatcher;
using Assets.Scripts.Utility;
using UnityEngine;

using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillCasters
{
    public class AISkillCaster : SkillCaster
    {
        public FixTimeDispatcher MinimumCoolDown;

        protected override void Update()
        {
            base.Update();
            UpdatePointingDirection();
        }

        public bool CanCastAnySkill()
        {
            return Skills.Any(s => s.CanActivate()) && MinimumCoolDown.CanDispatch() && !gameObject.HitPointAtZero();
        }

        void UpdatePointingDirection()
        {
            if (Target == null)
            {
                PointingDirection = MathUtility.GetFacingDirectionVector(GameView.FacingDirection);
            }
            else if (Skills.All(s => s.IsPassive || !s.IsActivate))
            {
                PointingDirection = MathUtility.GetDirection(transform.position, Target.position).normalized;
            }
        }

        [GameScriptEventAttribute(GameScriptEvent.AICastSkill)]
        public void CastSkill()
        {
            if (!CanCastAnySkill())
            {
                return;
            }

            int index = Random.Range(0, Skills.Count);
            while (!Skills[index].CanActivate())
            {
                index = Random.Range(0, Skills.Count);
            }

            Skills[index].Activate();
            MinimumCoolDown.Dispatch();
        }

        protected override void Deinitialize()
        {
        }
    }
}
