﻿using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Constants
{
    public enum InputKeyCode
    {
        VerticalAxis,
        HorizontalAxis,
        Attack1
    };

    public static class InputConstants
    {
        public const float DoubleClickBufferTime = 0.35f;

        private static readonly Dictionary<InputKeyCode, string> KeyCodeMapping = new Dictionary<InputKeyCode, string>()
        {
            {InputKeyCode.VerticalAxis, "VerticalAxis"},
            {InputKeyCode.HorizontalAxis, "HorizontalAxis"},
            {InputKeyCode.Attack1, "Attack1"}
        };

        public static string GetKeyCodeName(InputKeyCode keyCode)
        {
            if (KeyCodeMapping.ContainsKey(keyCode))
            {
                return KeyCodeMapping[keyCode];
            }
            else  //no input matches
            {
                throw new KeyNotFoundException();
            }
        }
	
        public static Vector3 GetMousePostition3D()
        {
            return Input.mousePosition;
        }

        public static Vector2 GetMousePostition2D()
        {
            return new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
    }
}
