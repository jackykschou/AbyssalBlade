using Assets.Scripts.Attributes;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.ObjectMotor
{
    public class PlayerCharacterMotor : CharacterMotor
    {
        [GameLogicEvent(Constants.GameLogicEvent.AxisMoved)]
        void MovePlayer(Vector2 direction)
        {
            RigidMove(direction);
        }
    }
}
