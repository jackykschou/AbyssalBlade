using Assets.Scripts.GameScripts.Components.GameValue;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.ObjectMotor
{
    public abstract class ObjectMotor2D : GameLogic
    {
        public GameValue Speed;

        private float _velocityX;
        private float _velocityY;

        protected override void Initialize()
        {
            base.Initialize();
            _velocityY = 0f;
            _velocityY = 0f;
        }

        protected override void Deinitialize()
        {
        }

        public void TranslateLinearTowards(Vector2 direction)
        {
            gameObject.transform.Translate(new Vector3(direction.x, direction.y, 0) * Speed * Time.deltaTime);
        }

        public void TranslateLinearTowards(GameObject target)
        {
            TranslateLinearTowards(gameObject.GetDirection(target));
        }

        public void TranslateSmoothTowards(Vector2 direction)
        {
            const float smoothDampSmoothness = 5.0f;

            Vector3 destination = transform.position + new Vector3(direction.x, direction.y, 0) * Speed * Time.deltaTime;

            float posX = Mathf.SmoothDamp(transform.position.x, destination.x, ref _velocityX, Time.deltaTime * smoothDampSmoothness);
            float posY = Mathf.SmoothDamp(transform.position.y, destination.y, ref _velocityY, Time.deltaTime * smoothDampSmoothness);

            transform.position = new Vector3(posX, posY, transform.position.z);
        }

        public void TranslateSmoothTowards(GameObject target)
        {
            TranslateSmoothTowards(gameObject.GetDirection(target));
        }

        public void InstantMoveTo(Vector2 position)
        {
            transform.position = new Vector3(position.x, position.y, transform.position.z);
        }

        public void MoveAlongWithStyle(EaseType style, Vector2 direction)
        {
            const float moveAlongDistance = 500;

            Vector2 destination = new Vector2(transform.position.x, transform.position.y) + direction * moveAlongDistance;
            float moveDuration = moveAlongDistance / Speed;
            gameObject.MoveTo(new Vector3(destination.x, destination.y, 0), moveDuration, 0, style);
        }

        public void MoveToWithStyle(EaseType style, Vector2 position)
        {
            float moveDuration = Vector3.Distance(new Vector3(position.x, position.y, 0), transform.position) / Speed;
            gameObject.MoveTo(new Vector3(position.x, position.y, 0), moveDuration, 0, style);
        }

        public void MoveAddWithStyle(EaseType style, Vector2 amount)
        {
            float moveDuration = amount.magnitude / Speed;
            gameObject.MoveAdd(new Vector3(amount.x, amount.y, 0), moveDuration, 0, style);
        }
    }
}
