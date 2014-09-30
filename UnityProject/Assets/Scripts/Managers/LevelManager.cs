using UnityEngine;

using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.Managers
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;

        public static bool LevelStarted 
        {
            get { return Instance != null && Instance._levelStarted; }
        }

        public GameObject PlayerMainCharacter;

        private bool _levelStarted;

        void Awake()
        {
            _levelStarted = false;
            Instance = FindObjectOfType<LevelManager>();
        }

        void OnDestroy()
        {
            Instance = null;
        }

        [GameEventAttribute(GameEvent.OnLevelFinishedLoading)]
        public void OnLevelFinishedLoading()
        {
            _levelStarted = true;
        }
    }
}
