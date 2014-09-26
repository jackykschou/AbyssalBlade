using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class LevelManager : MonoBehaviour
    {
        public const string MainCharacterGameObjectName = "MainCharacter";

        public static LevelManager Instance;

        public static bool LevelStarted 
        {
            get { return Instance != null && Instance._levelStarted; }
        }

        public GameObject PlayerMainCharacter;

        private bool _levelStarted;

        void Awake()
        {
            Instance = FindObjectOfType<LevelManager>();
            PlayerMainCharacter = GameObject.Find(MainCharacterGameObjectName);
        }

        void OnDestroy()
        {
            Instance = null;
        }
    }
}
