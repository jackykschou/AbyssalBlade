using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Constants
{
    public static class InputConstants
    {
        public const float DoubleClickBufferTime = 0.35f;

        public enum InputKeyCode
        {
            MoveUp,
            MoveDown,
            MoveLeft,
            MoveRight
        };

        private static readonly Dictionary<InputKeyCode, string> KeyCodeMapping = new Dictionary<InputKeyCode, string>()
        {
            {InputKeyCode.MoveUp, "Move Up"},
            {InputKeyCode.MoveDown, "Move Down"},
            {InputKeyCode.MoveLeft, "Move Left"},
            {InputKeyCode.MoveRight, "Move Right"}
        };

        public static string GetKeyCodeName(InputKeyCode keyCode)
        {
            if (KeyCodeMapping.ContainsKey(keyCode))
            {
                return KeyCodeMapping[keyCode];
            }
            else  //no input matches
            {
                return "";
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
