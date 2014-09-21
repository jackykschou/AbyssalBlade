using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Constants;
using UnityEngine;

using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;


namespace Assets.Scripts.GameScripts.GameLogic.Animator
{
    [RequireComponent(typeof(UnityEngine.Animator))]
    public abstract class ObjectAnimator : GameLogic
    {
        private Dictionary<string, float> _animationBoolParametesrAutoResetBufferMap;
        private string _lastBoolParameter;

        private const float BoolResetBufferFrameTime = 0.1f;

        public UnityEngine.Animator Animator;

        protected override void Initialize()
        {
            base.Initialize();
            Animator = GetComponent<UnityEngine.Animator>();
            _animationBoolParametesrAutoResetBufferMap = new Dictionary<string, float>();
            _lastBoolParameter = string.Empty;
        }

        protected override void Deinitialize()
        {
        }

        [GameScriptEventAttribute(GameScriptEvent.SetAnimatorState)]
        public void SetAnimatorBoolState(string state)
        {
            if (_lastBoolParameter != string.Empty)
            {
                Animator.SetBool(_lastBoolParameter, false);
            }
            _lastBoolParameter = state;
            Animator.SetBool(state, true);
            if (_animationBoolParametesrAutoResetBufferMap.ContainsKey(state))
            {
                _animationBoolParametesrAutoResetBufferMap[state] = BoolResetBufferFrameTime;
            }
            else
            {
                _animationBoolParametesrAutoResetBufferMap.Add(state, BoolResetBufferFrameTime);
            }
        }

        protected override void Update()
        {
            base.Update();
            List<string> keys = _animationBoolParametesrAutoResetBufferMap.Keys.ToList();
            foreach (var k in keys)
            {
                if (_animationBoolParametesrAutoResetBufferMap[k] <= 0)
                {
                    _animationBoolParametesrAutoResetBufferMap.Remove(k);
                    Animator.SetBool(k, false);
                }
                else
                {
                    _animationBoolParametesrAutoResetBufferMap[k] -= Time.deltaTime;
                }
            }
        }
    }
}
