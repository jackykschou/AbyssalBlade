using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.PhysicsBody
{
    [AddComponentMenu("PhysicsBody/Character/PlayerCharacterPhysicsBody")]
    public class PlayerCharacterPhysicsBody : CharacterPhysicsBody 
    {
        protected override void Initialize()
        {
            base.Initialize();
            gameObject.layer = LayerMask.NameToLayer(LayerConstants.LayerNames.PlayerCharacter);
        }
    }
}
