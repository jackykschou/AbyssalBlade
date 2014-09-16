namespace Assets.Scripts.GameScripts.GameLogic.PhysicsBody
{
    public abstract class ObstaclePhysicsBody : PhysicsBody2D
    {
        protected override void Initialize()
        {
            base.Initialize();
            Collider.isTrigger = false;
            Rigidbody.isKinematic = true;
        }
    }
}
