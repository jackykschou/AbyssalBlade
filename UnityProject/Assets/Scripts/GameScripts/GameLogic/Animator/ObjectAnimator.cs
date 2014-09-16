using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Animator
{
    [RequireComponent(typeof(UnityEngine.Animator))]
    public abstract class ObjectAnimator : GameLogic
    {
        private const int BoolResetBufferFrame = 2;
        private int _frameSinceLastAnimation;

        protected UnityEngine.Animator Animator;

        protected override void Initialize()
        {
            base.Initialize();
            _frameSinceLastAnimation = 0;
            Animator = GetComponent<UnityEngine.Animator>();
        }

        protected override void Deinitialize()
        {
        }

        public virtual void ResetAllBool()
        {
            Animator.SetBool(AnimatorControllerConstants.AnimatorParameterName.Idle, false);
        }

        public void SetAnimatorBoolState(string state)
        {
            Animator.SetBool(state, true);
            _frameSinceLastAnimation = BoolResetBufferFrame;
        }

        protected override void Update()
        {
            base.Update();
            if (_frameSinceLastAnimation > 0)
            {
                _frameSinceLastAnimation--;
                if (_frameSinceLastAnimation == 0)
                {
                    ResetAllBool();
                }
            }
        }
    }
}
