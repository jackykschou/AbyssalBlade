 using Assets.Scripts.Constants;
 using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [RequireComponent(typeof(Skill))]
    public abstract class SkillEffect : GameLogic
    {
        public Skill SKill;
        public bool Activated { get; protected set; }

        public virtual void Activate()
        {
            Activated = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            SKill = GetComponent<Skill>();
            gameObject.tag = SKill.tag;
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
