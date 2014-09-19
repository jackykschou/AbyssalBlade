using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

        private SortedDictionary<int, SkillEffect> _soretedSkillEffects;
            
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
            var skillEffects = _soretedSkillEffects.ToList();
            for (int i = 0; i < skillEffects.Count; ++i)
            {
                List<SkillEffect> activatedSkillEffect = new List<SkillEffect>();
                int order = skillEffects[i].Key;
                while ((i < skillEffects.Count) && skillEffects[i].Key == order)
                {
                    skillEffects[i].Value.Activate();
                    activatedSkillEffect.Add(skillEffects[i].Value);
                    i++;
                }
                i--;

                while (activatedSkillEffect.Any(e => e.Activated))
                {
                    yield return new WaitForSeconds(Time.deltaTime);
                }
            }
            IsActivate = false;
        }
        
        [GameScriptEventAttribute(GameScriptEvent.UpdateSkillCooldownPercentage)]
        public void UpdateCoolDownPercentage(Skill skill, float percentage)
        {
            _coolDownPercentage = percentage;
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
            _coolDownPercentage = 0f;
            _castableConditions = GetComponents<SkillCastableCondition>().ToList();
            gameObject.tag = Caster.gameObject.tag;
            _soretedSkillEffects = new SortedDictionary<int, SkillEffect>();
            for (int i = 0; i < _skillEffects.Count; ++i)
            {
                _soretedSkillEffects.Add(_skillEffectsOrder[i], _skillEffects[i]);
            }
        }

        protected override void Deinitialize()
        {
        }
    }
}
