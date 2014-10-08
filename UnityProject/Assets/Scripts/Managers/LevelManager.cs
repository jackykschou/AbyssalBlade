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
        public bool Debug;

        public const string MainCharacterGameObjectName = "MainCharacter";

        public bool IsPlayLevel;

        public static LevelManager Instance;

        public LoopName BackGroundMusicLoop;

        public static bool LevelStarted 
        {
            get { return Instance != null && Instance._levelStarted; }
        }

        public GameObject PlayerMainCharacter;

        public Transform CameraInitialFollowTransform;

        private bool _levelStarted;

        protected override void Initialize()
        {
            base.Initialize();
            _levelStarted = false;
            Instance = FindObjectOfType<LevelManager>();
            PlayerMainCharacter = GameObject.Find(MainCharacterGameObjectName);

            if (Debug)
            {
                TriggerGameEvent(GameEvent.OnLevelFinishedLoading);
            }
        }

        protected override void Deinitialize()
        {
            Instance = null;
        }

        public void SwitchLoopClip(int clipNumber)
        {
            AudioManager.Instance.swapLoopTrack(BackGroundMusicLoop);
        }

        [GameEventAttribute(GameEvent.OnLevelEnded)]
        public void OnLevelEnded()
        {
            AudioManager.Instance.stopLoop(BackGroundMusicLoop);
        }

        [GameEventAttribute(GameEvent.OnLevelFinishedLoading)]
        public void OnLevelFinishedLoading()
        {
            if (CameraInitialFollowTransform == null)
            {
                Camera.main.gameObject.TriggerGameScriptEvent(GameScriptEvent.CameraFollowTarget, PlayerMainCharacter.transform);
            }
            else
            {
                Camera.main.gameObject.TriggerGameScriptEvent(GameScriptEvent.CameraFollowTarget, CameraInitialFollowTransform);
            }
            AudioManager.Instance.playLoop(BackGroundMusicLoop);
            _levelStarted = true;
            GameManager.Instance.HUD.SetActive(IsPlayLevel);
            GameManager.Instance.PlayerMainCharacter.SetActive(IsPlayLevel);
        }
    }
}
