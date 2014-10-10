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

        public static GameManager Instance;

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
            LoadingScene.SetActive(true);
            HUD.SetActive(false);
            PlayerMainCharacter.SetActive(false);
            if (CurrentLevel != null)
            {
                PrefabManager.Instance.ImmediateDespawnPrefab(CurrentLevel);
            }
            yield return new WaitForSeconds(3.0f);
            GameEventManager.Instance.TriggerGameEvent(GameEvent.OnLevelStartLoading);
            CurrentLevel = PrefabManager.Instance.SpawnPrefab(levelPrefab);
            _currentLevelPrefab = levelPrefab;
            GameEventManager.Instance.TriggerGameEvent(GameEvent.OnLevelFinishedLoading);
            LoadingScene.SetActive(false);
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
            Instance = FindObjectOfType<GameManager>();
            DontDestroyOnLoad(Instance);
            PlayerMainCharacter = GameObject.Find(MainCharacterGameObjectName);
            PlayerMainCharacter.SetActive(false);
            HUD = GameObject.Find(HUDGameObjectName);
            HUD.SetActive(false);
            HUD.transform.position = Vector3.zero;
            MainCamera = GameObject.Find(MainCameraGameObjectName);
            LoadingScene = GameObject.Find(LoadingSceneGameObjectName);
            LoadingScene.SetActive(true);
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
