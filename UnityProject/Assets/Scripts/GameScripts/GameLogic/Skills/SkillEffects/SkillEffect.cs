 using Assets.Scripts.Constants;
 using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [RequireComponent(typeof(Skill))]
    public abstract class SkillEffect : GameLogic
    {
        public Skill Skill;
        public bool Activated { get; protected set; }

        public virtual void Activate()
        {
            Activated = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            Skill = GetComponent<Skill>();
            gameObject.tag = Skill.tag;
        }

        protected override void Deinitialize()
        {
        }

        public void TriggerCasterGameScriptEvent(GameScriptEvent Event, params object[] args)
        {
            Skill.Caster.TriggerGameScriptEvent(Event, args);
        }
    }
}
