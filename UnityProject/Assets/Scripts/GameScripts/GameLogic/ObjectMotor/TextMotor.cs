using Assets.Scripts.Constants;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.ObjectMotor
{
    [AddComponentMenu("2DObjectMotor/TextMotor")]
    public class TextMotor : ObjectMotor2D
    {
        public EaseType EaseType;

        public void Shoot(Vector3 dir, float speed, float distance)
        {
            MoveAlongWithStyle(EaseType, dir, speed, distance);
        }
    }
}
