﻿using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Animator
{
    [AddComponentMenu("Animator/PlayerCharacterAnimator")]
    public class PlayerCharacterAnimator : CharacterAnimator
    {
        public override void ResetAllBool()
        {
            base.ResetAllBool();
            Animator.SetBool(AnimatorControllerConstants.AnimatorParameterName.PlayerCharacterSkill1, false);
            Animator.SetBool(AnimatorControllerConstants.AnimatorParameterName.PlayerCharacterSkill2, false);
            Animator.SetBool(AnimatorControllerConstants.AnimatorParameterName.PlayerCharacterSkill3, false);
            Animator.SetBool(AnimatorControllerConstants.AnimatorParameterName.PlayerCharacterSkill4, false);
        }
    }
}
