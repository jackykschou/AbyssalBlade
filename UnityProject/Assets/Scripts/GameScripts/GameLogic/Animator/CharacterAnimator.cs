using Assets.Scripts.Attributes;
using Assets.Scripts.Constants;
using UnityEngine;
using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Animator
{
    [RequireComponent(typeof(UnityEngine.Animator))]
    public abstract class CharacterAnimator : ObjectAnimator
    {
        [GameScriptEventAttribute(GameScriptEvent.UpdateFacingDirection)]
        public void UpdateFacingDirection(FacingDirection facingDirection)
        {
            Animator.SetInteger(AnimatorControllerConstants.AnimatorParameterName.FacingDirection, (int)facingDirection);
        }

        [GameScriptEventAttribute(GameScriptEvent.OnObjectMove)]
        public void OnObjectMove()
        {
            SetAnimatorBoolState(AnimatorControllerConstants.AnimatorParameterName.Move);
        }

        [GameScriptEventAttribute(GameScriptEvent.OnDestroyableDestroyed)]
        public void PlayerDeathAnimation()
        {
            SetAnimatorBoolState(AnimatorControllerConstants.AnimatorParameterName.Death);
        }

        public override void ResetAllBool()
        {
            base.ResetAllBool();
            Animator.SetBool(AnimatorControllerConstants.AnimatorParameterName.Move, false);
            Animator.SetBool(AnimatorControllerConstants.AnimatorParameterName.Death, false);
        }

        protected override void Deinitialize()
        {
        }

    }
}
