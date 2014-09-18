﻿using System.Linq;
using Assets.Scripts.GameScripts.Components.TimeDispatcher;
using UnityEngine;

using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillCasters
{
    public class AISkillCaster : SkillCaster
    {
        public FixTimeDispatcher MinimumCoolDown;

        public bool CanCastAnySkill()
        {
            return Skills.Any(s => s.CanActivate()) && MinimumCoolDown.CanDispatch();
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
