using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic
{
    public class GameSetUpHelper : GameLogic
    {
        private const string HudPrefabName = "Prefabs/GameSetUp/MainHUD";
        private const string MainCharacterPrefabName = "Prefabs/GameSetUp/MainCharacter";
        private const string AudioManagerPrefabName = "Prefabs/GameSetUp/AudioManager";
        private const string MainCameraPrefabName = "Prefabs/GameSetUp/Main Camera";
        private const string GameManagerPrefabName = "Prefabs/GameSetUp/GameManager";
        private const string LoadingScreenPrefabName = "Prefabs/GameSetUp/LoadingScreen";
        private const string PrefabManagerPrefabName = "Prefabs/GameSetUp/PrefabManager";
        private const string AStarPrefabName = "Prefabs/GameSetUp/AStar";

        [SerializeField]
        private GameObject _hud;
        [SerializeField]
        private GameObject _mainCharacter;
        [SerializeField]
        private GameObject _audioManager;
        [SerializeField]
        private GameObject _mainCamera;
        [SerializeField]
        private GameObject _gameManager;
        [SerializeField]
        private GameObject _loadingScreen;
        [SerializeField]
        private GameObject _prefabManager;
        [SerializeField]
        private GameObject _aStar;


        protected override void Initialize()
        {
            base.Initialize();
            InstantiateGameObjects();
            GameManager.Instance.LoadingScene = _loadingScreen;
            GameManager.Instance.HUD = _hud;
            GameManager.Instance.PlayerMainCharacter = _mainCharacter;
            GameManager.Instance.MainCamera = _mainCamera;
        }

        private GameObject CreateGameObject(string gameObjectname)
        {
            GameObject o = Resources.Load(gameObjectname) as GameObject;
            GameObject instance = Instantiate(o, o.transform.position, Quaternion.identity) as GameObject;
            instance.name = o.name;
            return instance;
        }

        private void InstantiateGameObjects()
        {
            if (_hud == null)
            {
                _hud = CreateGameObject(HudPrefabName);
            }
            if (_mainCharacter == null)
            {
                _mainCharacter = CreateGameObject(MainCharacterPrefabName);
            }
            if (_audioManager == null)
            {
                _audioManager = CreateGameObject(AudioManagerPrefabName);
            }
            if (_mainCamera == null)
            {
                _mainCamera = CreateGameObject(MainCameraPrefabName);
            }
            if (_gameManager == null)
            {
                _gameManager = CreateGameObject(GameManagerPrefabName);
            }
            if (_loadingScreen == null)
            {
                _loadingScreen = CreateGameObject(LoadingScreenPrefabName);
            }
            if (_prefabManager == null)
            {
                _prefabManager = CreateGameObject(PrefabManagerPrefabName);
            }
            if (_aStar == null)
            {
                _aStar = CreateGameObject(AStarPrefabName);
            } 
        }

        public override void EditorUpdate()
        {
            base.EditorUpdate();
            InstantiateGameObjects();
        }

        protected override void Deinitialize()
        {
        }
    }
}
