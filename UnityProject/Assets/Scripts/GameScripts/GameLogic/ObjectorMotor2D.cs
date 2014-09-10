using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic
{
    public class ObjectorMotor2D : GameLogic 
    {
        protected override void Initialize()
        {
        }

        protected override void Deinitialize()
        {
        }

        public void TeleportTo(Vector2 location)
        {
            transform.position = new Vector3(location.x, location.y, transform.position.z);
        }
    }
}
