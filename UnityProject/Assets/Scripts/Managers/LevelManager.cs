using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic;
using Assets.Scripts.Utility;
using UnityEngine;

using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.Managers
{
    public class LevelManager : GameLogic
    {
        public bool IsPlayLevel;

        public static LevelManager Instance
        {
            get { return _instance ?? (_instance = FindObjectOfType<LevelManager>()); }
        }
        private static LevelManager _instance;

        public LoopName BackGroundMusicLoop;

        public static bool LevelStarted 
        {
            get { return Instance != null && Instance._levelStarted; }
        }

        public Transform CameraInitialFollowTransform;

        private bool _levelStarted;

        protected override void Initialize()
        {
            base.Initialize();
            _levelStarted = false;

            if (!GameManager.Instance.LoadLevelOnStart)
            {
                TriggerGameEvent(GameEvent.OnLevelFinishedLoading);
                TriggerGameEvent(GameEvent.OnLevelStarted);
            }
        }

        protected override void Deinitialize()
        {
            _instance = null;
        }

        [GameEventAttribute(GameEvent.OnLevelEnded)]
        public void OnLevelEnded()
        {
            AudioManager.Instance.StopLevelLoop(BackGroundMusicLoop);
        }

        [GameEventAttribute(GameEvent.OnLevelStarted)]
        public void OnLevelFinishedLoading()
        {
            if (CameraInitialFollowTransform == null)
            {
                GameManager.Instance.MainCamera.TriggerGameScriptEvent(GameScriptEvent.CameraFollowTarget, GameManager.Instance.PlayerMainCharacter.transform);
            }
            else
            {
                GameManager.Instance.MainCamera.TriggerGameScriptEvent(GameScriptEvent.CameraFollowTarget, CameraInitialFollowTransform);
            }
            _levelStarted = true;
            GameManager.Instance.HUD.SetActive(IsPlayLevel);
            GameManager.Instance.PlayerMainCharacter.SetActive(IsPlayLevel);
            AudioManager.Instance.PlayLevelLoop(BackGroundMusicLoop);
        }
    }
}
