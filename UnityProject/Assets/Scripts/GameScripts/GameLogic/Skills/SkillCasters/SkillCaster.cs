using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillCasters
{
    public abstract class SkillCaster : GameLogic
    {
        public List<Skill> Skills;
        public bool Casting;
        public Transform Target;

        protected override void Initialize()
        {
            base.Initialize();
            Skills = GetComponents<Skill>().ToList();
        }
    }
}
