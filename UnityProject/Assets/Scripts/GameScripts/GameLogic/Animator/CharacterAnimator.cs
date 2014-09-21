using Assets.Scripts.Constants;
using UnityEngine;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Animator
{
    [AddComponentMenu("ObjectAnimator/CharacterAnimator")]
    [RequireComponent(typeof(UnityEngine.Animator))]
    public class CharacterAnimator : ObjectAnimator
    {
        [GameScriptEventAttribute(GameScriptEvent.UpdateFacingDirection)]
        public void UpdateFacingDirection(FacingDirection facingDirection)
        {
            Animator.SetInteger(AnimatorControllerConstants.AnimatorParameterName.FacingDirection, (int)facingDirection);
        }

        [GameScriptEventAttribute(GameScriptEvent.OnCharacterMove)]
        public void OnObjectMove(Vector2 direction)
        {
            SetAnimatorBoolState(AnimatorControllerConstants.AnimatorParameterName.Move);
        }

        public void PlayerDeathAnimation()
        {
            SetAnimatorBoolState(AnimatorControllerConstants.AnimatorParameterName.Death);
        }

        protected override void Deinitialize()
        {
        }

    }
}
