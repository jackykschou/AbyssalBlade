namespace Assets.Scripts.GameScripts.GameLogic.PhysicsBody
{
    public abstract class CharacterPhysicsBody : PhysicsBody2D
    {
        protected override void Initialize()
        {
            base.Initialize();
            Rigidbody.isKinematic = false;
            Collider.isTrigger = false;
        }
    }
}
