﻿using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.Utility
{
    public static class MathUtility
    {
        public static Vector2 GetDirection(this GameObject obj, GameObject target)
        {
            return (target.transform.position - obj.transform.position).normalized;
        }

        public static FacingDirection GetFacingDirection(this Vector2 v)
        {
            if ((Vector2.Angle(Vector2.up, v) <= 45f) || (Vector2.Angle(v, Vector2.up) <= 45f))
            {
                return FacingDirection.Up;
            }
            if ((Vector2.Angle(Vector2.right, v) <= 45f) || (Vector2.Angle(v, Vector2.right) <= 45f))
            {
                return FacingDirection.Right;
            }
            if ((Vector2.Angle(-Vector2.up, v) <= 45f) || (Vector2.Angle(v, -Vector2.up) <= 45f))
            {
                return FacingDirection.Down;
            }
            return FacingDirection.Left;
        }
    }
}
