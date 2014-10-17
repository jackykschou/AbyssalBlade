using Assets.Scripts.GameScripts.Components.GameValue;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.GameValueTemporaryModifiers
{
    [AddComponentMenu("GameValueTemporaryModifier/CurrentPercentage")]
    public class GameValueTemporaryModifierCurrentPercentage : GameValueTemporaryModifier 
    {
        public float ModifyPercentage;

        public override float ChangeGameValue(GameValue gameValue)
        {
            float changedAmount = gameValue.Value * ModifyPercentage;
            gameValue.Value += changedAmount;
            return changedAmount;
        }
    }
}
