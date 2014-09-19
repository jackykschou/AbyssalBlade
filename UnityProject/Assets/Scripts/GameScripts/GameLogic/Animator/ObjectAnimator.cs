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
        private Dictionary<string, int> _animationBoolParametesrBufferMap;

        private const int BoolResetBufferFrame = 1;

        public UnityEngine.Animator Animator;

        protected override void Initialize()
        {
            base.Initialize();
            Animator = GetComponent<UnityEngine.Animator>();
            _animationBoolParametesrBufferMap = new Dictionary<string, int>();
        }

        protected override void Deinitialize()
        {
        }

        [GameScriptEventAttribute(GameScriptEvent.SetAnimatorState)]
        public void SetAnimatorBoolState(string state)
        {
            Animator.SetBool(state, true);
            if (_animationBoolParametesrBufferMap.ContainsKey(state))
            {
                _animationBoolParametesrBufferMap[state] = BoolResetBufferFrame;
            }
            else
            {
                _animationBoolParametesrBufferMap.Add(state, BoolResetBufferFrame);
            }
        }

        protected override void Update()
        {
            base.Update();
            List<string> keys = _animationBoolParametesrBufferMap.Keys.ToList();
            foreach (var k in keys)
            {
                if (_animationBoolParametesrBufferMap[k] <= 0)
                {
                    _animationBoolParametesrBufferMap.Remove(k);
                    Animator.SetBool(k, false);
                }
                else
                {
                    _animationBoolParametesrBufferMap[k]--;
                }
            }
        }
    }
}
