using UnityEngine;

namespace Assets.Scripts.Utility
{
    public static class MathUtility
    {
        public static Vector2 GetDirection(this GameObject obj, GameObject target)
        {
            return (target.transform.position - obj.transform.position).normalized;
        }
    }
}
