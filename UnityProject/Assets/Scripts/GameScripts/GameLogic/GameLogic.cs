using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Constants;
using Assets.Scripts.GameViews;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic
{
    [RequireComponent(typeof(GameLogicEventManager))]
    public abstract class GameLogic : GameScript
    {
        public GameLogicEventManager GameLogicEventManager {private get; set; }

        protected GameView GameView { get; set; }

        protected abstract override void Initialize();

        protected abstract override void Deinitialize();

        public void TriggerGameLogicEvnet(GameLogicEventConstants.GameLogicEvent gameLogicEvent, params object[] args)
        {
            GameLogicEventManager.TriggerGameLogicEvnet(gameLogicEvent, args);
        }

        public void TriggerGameLogicEvent<T>(GameLogicEventConstants.GameLogicEvent gameLogicEvent, params object[] args) where T : GameLogic
        {
            GameLogicEventManager.TriggerGameLogicEvent<T>(gameLogicEvent, args);
        }

        public void TriggerGameLogicEvent(GameLogic gameLogic, GameLogicEventConstants.GameLogicEvent gameLogicEvent, params object[] args)
        {
            GameLogicEventManager.TriggerGameLogicEvent(gameLogic, gameLogicEvent, args);
        }

        protected T GetGameLogic<T>() where T : GameLogic
        {
            return GetComponent<T>();
        }

        protected List<T> GetGameLogics<T>() where T : GameLogic
        {
            return GetComponents<T>().ToList();
        }
    }
}