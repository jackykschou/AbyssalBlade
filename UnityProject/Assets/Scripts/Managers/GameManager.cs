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
                PrefabManager.Instance.DespawnPrefab(CurrentLevel);
            }
            yield return new WaitForSeconds(3.0f);
            GameEventManager.Instance.TriggerGameEvent(GameEvent.OnLevelStartLoading);
            CurrentLevel = PrefabManager.Instance.SpawnPrefab(levelPrefab);
            _currentLevelPrefab = levelPrefab;
            GameEventManager.Instance.TriggerGameEvent(GameEvent.OnLevelFinishedLoading);
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

        [GameEventAttribute(GameEvent.OnLevelFinishedLoading)]
        public void OnLevelFinishedLoading()
        {
            LoadingScene.SetActive(false);
        }

        protected override void Initialize()
        {
            base.Initialize();
            Instance = FindObjectOfType<GameManager>();
            DontDestroyOnLoad(Instance);
            PlayerMainCharacter = GameObject.Find(MainCharacterGameObjectName);
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
