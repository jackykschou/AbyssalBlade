using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic;
using UnityEngine;

using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.Managers
{
    public class LevelManager : GameLogic
    {
        public static LevelManager Instance;

        public LoopName BackGroundMusicLoop;

        public static bool LevelStarted 
        {
            get { return Instance != null && Instance._levelStarted; }
        }

        public GameObject PlayerMainCharacter;

        private bool _levelStarted;

        protected override void Initialize()
        {
            base.Initialize();
            _levelStarted = false;
            Instance = FindObjectOfType<LevelManager>();
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
            AudioManager.Instance.playLoop(BackGroundMusicLoop);
            _levelStarted = true;
        }
    }
}
