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
        public GameObject MainCamera;

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

        public GameObject LoadingScene;

        public GameObject HUD;

        public GameObject PlayerMainCharacter;

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
            LoadingScene.SetActive(true);
            if (CurrentLevel != null)
            {
                PrefabManager.Instance.ImmediateDespawnPrefab(CurrentLevel);
            }
            GameEventManager.Instance.TriggerGameEvent(GameEvent.OnLevelStartLoading);
            PrefabManager.Instance.SpawnPrefabImmediate(levelPrefab, o => { CurrentLevel = o; });
            _currentLevelPrefab = levelPrefab;
            GameEventManager.Instance.TriggerGameEvent(GameEvent.OnLevelFinishedLoading);
            yield return new WaitForSeconds(0.2f);
            LoadingScene.SetActive(false);
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
            if (PlayerMainCharacter == null)
            {
                PlayerMainCharacter = GameObject.Find(MainCharacterGameObjectName);
            }
            if (HUD == null)
            {
                HUD = GameObject.Find(HUDGameObjectName);
            }
            HUD.transform.position = Vector3.zero;
            if (MainCamera == null)
            {
                MainCamera = GameObject.Find(MainCameraGameObjectName);
            }
            if (LoadingScene == null)
            {
                LoadingScene = GameObject.Find(LoadingSceneGameObjectName);
            }
            if (LoadLevelOnStart)
            {
                ChangeLevel(StartingLevelPrefab);
            }
        }

        protected override void Deinitialize()
        {
        }
    }
}
