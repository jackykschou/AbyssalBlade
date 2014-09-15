using Assets.Scripts.Attributes;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.ObjectMotor
{
    [AddComponentMenu("2DObjectMotor/PlayerCharacterMotor")]
    public class PlayerCharacterMotor : CharacterMotor
    {
        [GameScriptEvent(Constants.GameScriptEvent.AxisMoved)]
        void MovePlayer(Vector2 direction)
        {
            RigidMove(direction);
        }
    }
}
