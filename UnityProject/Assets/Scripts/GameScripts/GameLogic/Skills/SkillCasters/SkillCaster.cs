using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillCasters
{
    public abstract class SkillCaster : GameLogic
    {
        public List<Skill> Skills;
        public bool CastingActiveSkill
        {
            get { return Skills.Any(s => s.IsActivate || s.IsPassive); }
        }

        public Transform Target;
        public Vector2 PointingDirection;

        protected override void Initialize()
        {
            base.Initialize();
            Skills = GetComponentsInChildren<Skill>().ToList();
        }

        [GameScriptEventAttribute(GameScriptEvent.OnNewTargetDiscovered)]
        public void UpdateTarget(GameObject target)
        {
            Target = target.transform;
        }
    }
}
