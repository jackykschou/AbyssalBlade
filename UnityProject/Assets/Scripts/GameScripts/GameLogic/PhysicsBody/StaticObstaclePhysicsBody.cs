using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.PhysicsBody
{
    [AddComponentMenu("PhysicsBody/Obstacle/ObstaclePhysicsBody")]
    public class StaticObstaclePhysicsBody : ObstaclePhysicsBody 
    {
        protected override void Initialize()
        {
            base.Initialize();
            gameObject.layer = LayerMask.NameToLayer(LayerConstants.LayerNames.StaticObstacle);
        }
    }
}
