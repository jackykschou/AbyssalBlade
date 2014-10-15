using Assets.Scripts.Constants;
using Assets.Scripts.Utility;
using StateMachine.Action;
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
            MoveByWithStyle(EaseType, dir * distance, speed);
            rigidbody2D.gravityScale = .1f;
            rigidbody2D.isKinematic = false;
            rigidbody2D.fixedAngle = true;
        }

        public void Shoot(EaseType type, Vector3 dir, float speed, float distance)
        {
            MoveByWithStyle(type, dir * distance, speed);
            rigidbody2D.gravityScale = 0.0f;
            rigidbody2D.isKinematic = false;
            rigidbody2D.fixedAngle = true;
        }

        public void ShootAdd(EaseType type, Vector3 dir, float speed, float distance)
        {
            MoveAddWithStyle(type, dir * distance, speed);
        }
    }
}
