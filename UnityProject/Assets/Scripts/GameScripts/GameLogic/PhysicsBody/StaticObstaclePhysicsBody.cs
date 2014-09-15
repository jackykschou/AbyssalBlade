using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.PhysicsBody
{
    [AddComponentMenu("PhysicsBody/StaticObstaclePhysicsBody")]
    public class StaticObstaclePhysicsBody : PhysicsBody2D
    {
        protected override void Initialize()
        {
            base.Initialize();
            gameObject.layer = LayerMask.NameToLayer(LayerConstants.LayerNames.StaticObstacle);
        }
    }
}
