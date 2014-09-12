using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.CastableCondition
{
    [RequireComponent(typeof(Skill))]
    public abstract class SkillCastableCondition : GameLogic
    {
        public abstract bool CanCast();
    }
}
