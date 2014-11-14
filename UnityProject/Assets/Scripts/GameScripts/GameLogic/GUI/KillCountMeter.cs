using System.Runtime.InteropServices;
using Assets.Scripts.Managers;
using StateMachine.Action;
using UnityEngine;
using UnityEngine.UI;
using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.GUI
{
    [RequireComponent(typeof(Text))]
    public class KillCountMeter : GameLogic
    {
        private Text text;
        private int currentKillCount;

        protected override void Initialize()
        {
            base.Initialize();
            text = GetComponent<Text>();
            currentKillCount = 0;
            text.text = currentKillCount.ToString();
        }

        protected override void Deinitialize()
        {
        }

        [GameEventAttribute(GameEvent.OnSectionEnemyDespawned)]
        public void IncreaseCount(int sectionId)
        {
            currentKillCount++;
            text.text = currentKillCount.ToString();
        }

        [GameEventAttribute(GameEvent.OnLevelStarted)]
        public void Reset()
        {
            currentKillCount = 0;
            text.text = currentKillCount.ToString();
        }
    }
}
