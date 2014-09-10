using UnityEngine;

using GameLogicEventAttribute = Assets.Scripts.Attributes.GameLogicEvent;

namespace Assets.Scripts.GameScripts.GameLogic
{
    public class ObjectMotor2D : GameLogic
    {
        private float _velocityX;
        private float _velocityY;

        protected override void Initialize()
        {
            _velocityY = 0f;
            _velocityY = 0f;
        }

        protected override void Deinitialize()
        {
        }

        public void MoveTowardsLinear(Vector2 direction, float speed)
        {
            transform.Translate(new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime);
        }

        public void MoveTowardsSmooth(Vector2 direction, float speed)
        {
            const float smoothDampSmoothness = 5.0f;

            Vector3 destination = transform.position + new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;

            float posX = Mathf.SmoothDamp(transform.position.x, destination.x, ref _velocityX, Time.deltaTime * smoothDampSmoothness);
            float posY = Mathf.SmoothDamp(transform.position.y, destination.y, ref _velocityY, Time.deltaTime * smoothDampSmoothness);

            transform.position = new Vector3(posX, posY, transform.position.z);
        } 

        public void TeleportTo(Vector2 position)
        {
            transform.position = new Vector3(position.x, position.y, transform.position.z);
        }
    }
}
