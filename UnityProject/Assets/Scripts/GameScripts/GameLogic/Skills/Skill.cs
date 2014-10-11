using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.GameScripts.GameLogic.Skills.CastableCondition;
using Assets.Scripts.GameScripts.GameLogic.Skills.SkillCasters;
using Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects;
using Assets.Scripts.Utility;
using UnityEngine;

using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Skills
{
    [AddComponentMenu("Skill/Skill")]
    public sealed class Skill : GameLogic
    {
        public SkillCaster Caster { get; private set; }
        public bool IsActivate;
        public bool OnceAtATime;
        public bool IsPassive;

        [SerializeField] 
        private List<SkillEffect> _skillEffects;

        [SerializeField] 
        private List<int> _skillEffectsOrder;

        private SortedDictionary<int, List<SkillEffect>> _soretedSkillEffects;
            
        [Range(0f, 1f)] 
        private float _coolDownPercentage;

        private List<SkillCastableCondition> _castableConditions;

        public override void EditorUpdate()
        {
            base.EditorUpdate();
            if (_skillEffects == null)
            {
                return;
            }
            if (_skillEffects.Count != _skillEffectsOrder.Count)
            {
                _skillEffectsOrder.Resize(_skillEffects.Count);
            }
        }

        public bool CanActivate()
        {
            return _castableConditions.All(c => c.CanCast()) && (!OnceAtATime || !IsActivate) && (IsPassive || !Caster.CastingActiveSkill);
        }

        public void Activate()
        {
            if (CanActivate())
            {
                IsActivate = true;
                Caster.TriggerGameScriptEvent(GameScriptEvent.SkillCastTriggerSucceed, this);
                StartCoroutine(ActivateSkillEffects());
            }
            else
            {
                Caster.TriggerGameScriptEvent(GameScriptEvent.SkillCastTriggerFailed, this);
            }
        }

        IEnumerator ActivateSkillEffects()
        {
            foreach (var pair in _soretedSkillEffects)
            {
                pair.Value.ForEach(e => e.Activate());

                while (pair.Value.Any(e => e.Activated))
                {
                    yield return new WaitForSeconds(Time.deltaTime);
                }
            }
            Caster.TriggerGameScriptEvent(GameScriptEvent.SkillCastFinished, this);
            IsActivate = false;
        }
        
        [GameScriptEventAttribute(GameScriptEvent.UpdateSkillCooldownPercentage)]
        public void UpdateCoolDownPercentage(Skill skill, float percentage)
        {
            _coolDownPercentage = percentage;
        }

        public float getCooldownPercentage()
        {
            return _coolDownPercentage;
        }

        protected override void Initialize()
        {
            base.Initialize();
            if (transform.parent == null || transform.parent.gameObject.GetComponent<SkillCaster>() == null)
            {
                throw new Exception("A Skill must have a parent that is the caster");
            }
            IsActivate = false;
            Caster = transform.parent.gameObject.GetComponent<SkillCaster>();
            _coolDownPercentage = 1f;
            _castableConditions = GetComponents<SkillCastableCondition>().ToList();
            gameObject.tag = Caster.gameObject.tag;
            _soretedSkillEffects = new SortedDictionary<int, List<SkillEffect>>();
            for (int i = 0; i < _skillEffects.Count; ++i)
            {
                if (!_soretedSkillEffects.ContainsKey(_skillEffectsOrder[i]))
                {
                    _soretedSkillEffects.Add(_skillEffectsOrder[i], new List<SkillEffect>());
                }
                _soretedSkillEffects[_skillEffectsOrder[i]].Add(_skillEffects[i]);
            }
        }

        protected override void Deinitialize()
        {
        }
    }
}
