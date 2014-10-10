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

        protected override void Initialize()
        {
            base.Initialize();
            CreateGameObject(HudPrefabName);
            CreateGameObject(MainCharacterPrefabName);
            CreateGameObject(AudioManagerPrefabName);
            CreateGameObject(MainCameraPrefabName);
            CreateGameObject(LoadingScreenPrefabName);
            CreateGameObject(PrefabManagerPrefabName);
            CreateGameObject(AStarPrefabName);
            CreateGameObject(GameManagerPrefabName);
        }

        private void CreateGameObject(string gameObjectname)
        {
            GameObject o = Resources.Load(gameObjectname) as GameObject;
            Instantiate(o, o.transform.position, Quaternion.identity).name = o.name;
        }

        protected override void Deinitialize()
        {
        }
    }
}
