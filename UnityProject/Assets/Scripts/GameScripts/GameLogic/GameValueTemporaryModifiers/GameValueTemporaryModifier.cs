using System.Collections.Generic;
using Assets.Scripts.GameScripts.Components.GameValue;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.GameValueTemporaryModifiers
{
    public abstract class GameValueTemporaryModifier : GameLogic
    {
        public abstract float ChangeGameValue(GameValue gameValue);
        
        private Dictionary<GameValue, float> _changedAmountMap;

        protected override void Initialize()
        {
            base.Initialize();
            _changedAmountMap = new Dictionary<GameValue, float>();
        }

        protected override void Deinitialize()
        {
        }

        public void Modify(GameValue gameValue)
        {
            if (_changedAmountMap.ContainsKey(gameValue))
            {
                return;
            }
            _changedAmountMap.Add(gameValue, ChangeGameValue(gameValue));
        }

        public void Unmodify(GameValue gameValue)
        {
            if (!_changedAmountMap.ContainsKey(gameValue))
            {
                return;
            }
            gameValue.Value -= _changedAmountMap[gameValue];
            _changedAmountMap.Remove(gameValue);
        }
    }
}
