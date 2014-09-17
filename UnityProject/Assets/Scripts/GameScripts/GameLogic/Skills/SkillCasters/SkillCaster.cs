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
        public bool Casting
        {
            get { return Skills.Any(s => s.IsActivate); }
        }

        public Transform Target;

        protected override void Initialize()
        {
            base.Initialize();
            Skills = GetComponents<Skill>().ToList();
        }

        [GameScriptEventAttribute(GameScriptEvent.OnNewTargetDiscovered)]
        public void UpdateTarget(GameObject target)
        {
            Target = target.transform;
        }
    }
}
