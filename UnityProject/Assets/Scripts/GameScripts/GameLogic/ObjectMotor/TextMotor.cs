using Assets.Scripts.Constants;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.ObjectMotor
{
    [AddComponentMenu("2DObjectMotor/TextMotor")]
    [RequireComponent(typeof(Rigidbody2D))]
    public class TextMotor : ObjectMotor2D
    {
        public EaseType EaseType;

        public void Shoot(Vector3 dir, float speed, float distance)
        {
            MoveAlongWithStyle(EaseType, dir, speed, distance);
            rigidbody2D.gravityScale = .1f;
            rigidbody2D.isKinematic = false;
            rigidbody2D.fixedAngle = true;
        }
    }
}
