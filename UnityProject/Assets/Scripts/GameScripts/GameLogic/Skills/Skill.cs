﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Constants;
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
        public bool Movable;

        [SerializeField] 
        private List<SkillEffect> _skillEffects;
        [SerializeField] 
        private List<SkillEffect> _proSkillEffects;
        [SerializeField] 
        private List<int> _skillEffectsOrder;
        [SerializeField]
        private List<int> _proSkillEffectsOrder;

        private SortedDictionary<int, List<SkillEffect>> _soretedSkillEffects;
        private SortedDictionary<int, List<SkillEffect>> _soretedProSkillEffects;
            
        [Range(0f, 1f)] 
        private float _coolDownPercentage;

        private List<SkillCastableCondition> _castableConditions;
        private Vector2 _direction;

        public override void EditorUpdate()
        {
            base.EditorUpdate();
            if (_skillEffects != null && _skillEffects.Count != _skillEffectsOrder.Count)
            {
                _skillEffectsOrder.Resize(_skillEffects.Count);
            }
            if (_proSkillEffects != null && _proSkillEffects.Count != _proSkillEffectsOrder.Count)
            {
                _proSkillEffectsOrder.Resize(_proSkillEffects.Count);
            }
        }

        public bool CanActivate()
        {
            return _castableConditions.All(c => c.CanCast()) && (!OnceAtATime || !IsActivate) && (IsPassive || !Caster.CastingActiveSkill) && (Movable || !Caster.Moving);
        }

        public void Activate()
        {
            if (CanActivate())
            {
                UpdateFacingDirection();
                IsActivate = true;
                Caster.TriggerGameScriptEvent(GameScriptEvent.SkillCastTriggerSucceed, this);
                StartCoroutine(ActivateSkillEffects());
            }
            else
            {
                Caster.TriggerGameScriptEvent(GameScriptEvent.SkillCastTriggerFailed, this);
            }
        }

        public void UpdateFacingDirection()
        {
            _direction = _direction.normalized;
            FacingDirection newDirection = _direction.GetFacingDirection();
            Caster.TriggerGameScriptEvent(GameScriptEvent.UpdateFacingDirection, newDirection);
        }

        IEnumerator ActivateSkillEffects()
        {
            foreach (var pair in _soretedSkillEffects)
            {
                pair.Value.ForEach(
                    e =>
                    {
                        if (e.CanActivate())
                        {
                            e.Activate();
                        }
                    });

                while (pair.Value.Any(e => e.Activated))
                {
                    yield return new WaitForSeconds(Time.deltaTime);
                }
            }
            Caster.TriggerGameScriptEvent(GameScriptEvent.SkillCastFinished, this);
            IsActivate = false;
            StartCoroutine(ActivateProSkillEffects());
        }

        IEnumerator ActivateProSkillEffects()
        {
            foreach (var pair in _soretedProSkillEffects)
            {
                pair.Value.ForEach(
                    e =>
                    {
                        if (e.CanActivate())
                        {
                            e.Activate();
                        }
                    });

                while (pair.Value.Any(e => e.Activated))
                {
                    yield return new WaitForSeconds(Time.deltaTime);
                }
            }
        }
        
        [GameScriptEventAttribute(GameScriptEvent.UpdateSkillCooldownPercentage)]
        public void UpdateCoolDownPercentage(Skill skill, float percentage)
        {
            _coolDownPercentage = percentage;
        }

        public float GetCooldownPercentage()
        {
            return _coolDownPercentage;
        }

        [GameScriptEventAttribute(GameScriptEvent.UpdatePlayerAxis)]
        void UpdateMoveDirection(Vector2 direction)
        {
            _direction = direction;
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
            if (_skillEffects == null || _skillEffects.Count == 0)
            {
                _skillEffects = new List<SkillEffect>();
            }
            else
            {
                for (int i = 0; i < _skillEffects.Count; ++i)
                {
                    if (!_soretedSkillEffects.ContainsKey(_skillEffectsOrder[i]))
                    {
                        _soretedSkillEffects.Add(_skillEffectsOrder[i], new List<SkillEffect>());
                    }
                    _soretedSkillEffects[_skillEffectsOrder[i]].Add(_skillEffects[i]);
                }
            }

            _soretedProSkillEffects = new SortedDictionary<int, List<SkillEffect>>();
            if (_proSkillEffects == null || _proSkillEffects.Count == 0)
            {
                _proSkillEffects = new List<SkillEffect>();
            }
            else
            {
                for (int i = 0; i < _proSkillEffects.Count; ++i)
                {
                    if (!_soretedProSkillEffects.ContainsKey(_proSkillEffectsOrder[i]))
                    {
                        _soretedProSkillEffects.Add(_proSkillEffectsOrder[i], new List<SkillEffect>());
                    }
                    _soretedProSkillEffects[_proSkillEffectsOrder[i]].Add(_proSkillEffects[i]);
                }
            }
        }

        protected override void Deinitialize()
        {
        }
    }
}
