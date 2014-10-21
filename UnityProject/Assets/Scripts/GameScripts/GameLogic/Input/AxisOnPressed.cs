﻿using System.Linq;
using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Input
{
    [System.Serializable]
    public class AxisOnPressed : ButtonOnPressed 
    {
        public float GetAxisValue()
        {
            float max = KeyCodes.Max(c => UnityEngine.Input.GetAxis(InputConstants.GetKeyCodeName(c)));
            float min = KeyCodes.Min(c => UnityEngine.Input.GetAxis(InputConstants.GetKeyCodeName(c)));
            return Mathf.Abs(max) > Mathf.Abs(min) ? max : min;
        }
    }
}
