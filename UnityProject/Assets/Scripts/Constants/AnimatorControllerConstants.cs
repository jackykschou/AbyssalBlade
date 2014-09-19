using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Constants
{
    public static class AnimatorControllerConstants
    {
        public static class AnimatorParameterName
        {
            public const string FacingDirection = "FacingDirection";
            public const string Idle = "Idle";
            public const string Move = "Move";
            public const string Death = "Death";
            public const string PlayerCharacterSkill1 = "Skill1";
            public const string PlayerCharacterSkill2 = "Skill2";
            public const string PlayerCharacterSkill3 = "Skill3";
            public const string PlayerCharacterSkill4 = "Skill4";
            public const string EnemyBasicMelee = "Melee";
            public const string EnemyBasicRangeAttack = "RangeAttack";
        }

        private const string AnimatorsPath = "AnimatorController/";

        private static Dictionary<AnimatorName, string> animatorNameMap = new Dictionary<AnimatorName, string>();

        public enum AnimatorName
        {

        };

        public static RuntimeAnimatorController LoadAnimatorController(AnimatorName name)
        {
            if (!animatorNameMap.ContainsKey(name))
            {
                throw new Exception("Animator name is not defined");
            }

            RuntimeAnimatorController controller = Resources.Load(AnimatorsPath + animatorNameMap[name]) as RuntimeAnimatorController;

            if(controller == null)
            {
                throw new Exception("AnimatorController not found");
            }

            return controller;
        }
    }
}
