using Assets.Scripts.Attributes;
using Assets.Scripts.Constants;
using UnityEngine;
using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Animator
{
    [AddComponentMenu("Animator/CharacterAnimator")]
    [RequireComponent(typeof(UnityEngine.Animator))]
    public class CharacterAnimator : GameLogic
    {
        private const int BoolResetBufferFrame = 2;
        private int _frameSinceLastAnimation;

        private UnityEngine.Animator _animator;

        protected override void Initialize()
        {
            base.Initialize();
            _frameSinceLastAnimation = 0;
            _animator = GetComponent<UnityEngine.Animator>();
        }

        [Attributes.GameScriptEvent(GameScriptEvent.UpdateFacingDirection)]
        public void UpdateFacingDirection(FacingDirection facingDirection)
        {
            _animator.SetInteger(AnimatorControllerConstants.AnimatorParameterName.FacingDirection, (int)facingDirection);
        }

        [Attributes.GameScriptEvent(GameScriptEvent.OnObjectMove)]
        public void OnObjectMove()
        {
            _animator.SetBool(AnimatorControllerConstants.AnimatorParameterName.Move, true);
        }

        public virtual void ResetAllBool()
        {
            _animator.SetBool(AnimatorControllerConstants.AnimatorParameterName.Idle, false);
            _animator.SetBool(AnimatorControllerConstants.AnimatorParameterName.Move, false);
            _animator.SetBool(AnimatorControllerConstants.AnimatorParameterName.Death, false);
        }

        protected override void Deinitialize()
        {
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
