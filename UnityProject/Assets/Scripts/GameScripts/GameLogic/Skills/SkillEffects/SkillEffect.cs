 using Assets.Scripts.Constants;
 using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [RequireComponent(typeof(Skill))]
    public abstract class SkillEffect : GameLogic
    {
        public Skill SKill;

        protected override void Initialize()
        {
            base.Initialize();
            SKill = GetComponent<Skill>();
        }

        protected override void Deinitialize()
        {
        }

        public void TriggerCasterGameScriptEvent(GameScriptEvent Event, params object[] args)
        {
            SKill.Caster.TriggerGameScriptEvent(Event, args);
        }
    }
}
