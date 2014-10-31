using System.Collections;
using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic;
using UnityEngine;

using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.Managers
{
    public class GameManager : GameLogic
    {
        public bool LoadLevelOnStart = true;

        public const string MainCharacterGameObjectName = "MainCharacter";
        public const string HUDGameObjectName = "MainHUD";
        public const string MainCameraGameObjectName = "Main Camera";
        public const string LoadingSceneGameObjectName = "LoadingScreen";

        public static GameManager Instance {
            get { return _instance ?? (_instance = FindObjectOfType<GameManager>()); }
        }
        private static GameManager _instance;

        public Prefab StartingLevelPrefab;

        public GameObject CurrentLevel;

        private Prefab _currentLevelPrefab;

        private GameObject _mainCamera;
        public GameObject MainCamera
        {
            get
            {
                if (_mainCamera == null)
                {
                    _mainCamera = GameObject.Find(MainCameraGameObjectName);
                }
                return _mainCamera;
            }
        }

        private GameObject _loadingScreen;
        public GameObject LoadingScreen
        {
            get
            {
                if (_loadingScreen == null)
                {
                    _loadingScreen = GameObject.Find(LoadingSceneGameObjectName);
                }
                return _loadingScreen;
            }
        }

        private GameObject _HUD;
        public GameObject HUD
        {
            get
            {
                if (_HUD == null)
                {
                    _HUD = GameObject.Find(HUDGameObjectName);
                }
                return _HUD;
            }
        }

        private GameObject _playerMainCharacter;
        public GameObject PlayerMainCharacter
        {
            get
            {
                if (_playerMainCharacter == null)
                {
                    _playerMainCharacter = GameObject.Find(MainCharacterGameObjectName);
                }
                return _playerMainCharacter;
            }
        }

        [Range(0, 10)]
        public int Difficulity;

        public void ChangeLevel(Prefab levelPrefab)
        {
            StartCoroutine(ChangeLevelIE(levelPrefab));
        }

        private IEnumerator ChangeLevelIE(Prefab levelPrefab)
        {
            GameEventManager.Instance.TriggerGameEvent(GameEvent.OnLevelEnded);
            HUD.SetActive(false);
            PlayerMainCharacter.SetActive(false);
            ShowLoadingScreen();
            AudioManager.Instance.Mute();
            yield return new WaitForSeconds(1.0f);
            if (CurrentLevel != null)
            {
                PrefabManager.Instance.ImmediateDespawnPrefab(CurrentLevel);
            }
            GameEventManager.Instance.TriggerGameEvent(GameEvent.OnLevelStartLoading);
            PrefabManager.Instance.SpawnPrefabImmediate(levelPrefab, o => { CurrentLevel = o; });
            _currentLevelPrefab = levelPrefab;
            yield return new WaitForSeconds(1.0f);
            GameEventManager.Instance.TriggerGameEvent(GameEvent.OnLevelFinishedLoading);
            yield return new WaitForSeconds(1.0f);
            HideLoadingScreen();
            AudioManager.Instance.UnMute();
            GameEventManager.Instance.TriggerGameEvent(GameEvent.OnLevelStarted);
        }

        public void DestroyScene(GameObject sceneGameObject)
        {
            foreach (Transform t in sceneGameObject.transform)
            {
                DestroyScene(t.gameObject);
            }
            Destroy(sceneGameObject);
        }

        public void ReloadLevel()
        {
            ChangeLevel(_currentLevelPrefab);
        }

        protected override void Initialize()
        {
            base.Initialize();
            DontDestroyOnLoad(Instance);
            if (LoadLevelOnStart)
            {
                ChangeLevel(StartingLevelPrefab);
            }
        }

        protected override void Deinitialize()
        {
        }

        public void ShowLoadingScreen()
        {
            LoadingScreen.SetActive(true);
        }

        public void HideLoadingScreen()
        {
            LoadingScreen.SetActive(false);
        }
    }
}
