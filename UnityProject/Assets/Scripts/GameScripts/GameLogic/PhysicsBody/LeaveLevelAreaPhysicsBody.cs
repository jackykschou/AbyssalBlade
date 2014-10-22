using Assets.Scripts.Constants;

namespace Assets.Scripts.GameScripts.GameLogic.PhysicsBody
{
    public class LeaveLevelAreaPhysicsBody : PhysicsBody2D
    {
        protected override void Initialize()
        {
            base.Initialize();
            gameObject.layer = LayerConstants.LayerMask.LeaveLevelArea;
            Collider.isTrigger = true;
            Rigidbody.isKinematic = true;
        }
    }
}
