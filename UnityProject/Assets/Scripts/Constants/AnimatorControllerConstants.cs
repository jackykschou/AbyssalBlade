using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Constants
{
    public static class AnimatorControllerConstants
    {
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
