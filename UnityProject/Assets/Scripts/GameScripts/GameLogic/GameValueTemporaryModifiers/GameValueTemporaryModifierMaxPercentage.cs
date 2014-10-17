using Assets.Scripts.GameScripts.Components.GameValue;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.GameValueTemporaryModifiers
{
    [AddComponentMenu("GameValueTemporaryModifier/MaxPercentage")]
    public class GameValueTemporaryModifierMaxPercentage : GameValueTemporaryModifier 
    {
        public float ModifyPercentage;

        public override float ChangeGameValue(GameValue gameValue)
        {
            float changedAmount = gameValue.Max * ModifyPercentage;
            gameValue.Value += changedAmount;
            return changedAmount;
        }
    }
}
