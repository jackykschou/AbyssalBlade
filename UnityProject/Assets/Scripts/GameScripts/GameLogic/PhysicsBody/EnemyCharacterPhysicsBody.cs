using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.PhysicsBody
{
    [AddComponentMenu("PhysicsBody/Character/EnemyCharacterPhysicsBody")]
    public class EnemyCharacterPhysicsBody : CharacterPhysicsBody
    {
        protected override void Initialize()
        {
            base.Initialize();
            gameObject.layer = LayerMask.NameToLayer(LayerConstants.LayerNames.Enemy);
        }
    }
}
