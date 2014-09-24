using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.PhysicsBody
{
    [AddComponentMenu("PhysicsBody/Obstacle/InvisibleWallPhysicsBody")]
    public class InvisibleWallPhysicsBody : ObstaclePhysicsBody
    {
        protected override void Initialize()
        {
            base.Initialize();
            gameObject.layer = LayerMask.NameToLayer(LayerConstants.LayerNames.InvisibleWall);
        }
    }
}
