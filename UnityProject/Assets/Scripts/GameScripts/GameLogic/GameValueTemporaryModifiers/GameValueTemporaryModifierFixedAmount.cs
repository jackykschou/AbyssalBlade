using Assets.Scripts.GameScripts.Components.GameValue;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.GameValueTemporaryModifiers
{
    [AddComponentMenu("GameValueTemporaryModifier/FixedAmount")]
    public class GameValueTemporaryModifierFixedAmount : GameValueTemporaryModifier
    {
        public float ModifyAmount;

        public override float ChangeGameValue(GameValue gameValue)
        {
            gameValue.Value += ModifyAmount;
            return ModifyAmount;
        }
    }
}
