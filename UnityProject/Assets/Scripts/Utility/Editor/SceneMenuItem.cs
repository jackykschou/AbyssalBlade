using UnityEditor;

namespace Assets.Scripts.Utility.Editor
{
    public class SceneMenuItem : UnityEditor.Editor
    {
        private const string SceneFolderPath = "Assets/Scenes/";
        private const string SceneFileExtension = ".unity";

        private const string TestSceneFolder = "Tests/";

        [MenuItem("Open Scene/Testing/GameLogicComponentEventTest")]
        public static void OpenGameLogicComponentEventTest()
        {
            OpenScene(TestSceneFolder + "GameLogicComponentEventTest");
        }

        private static void OpenScene(string scenePath)
        {
            if (EditorApplication.SaveCurrentSceneIfUserWantsTo())
            {
                EditorApplication.OpenScene(SceneFolderPath + scenePath + SceneFileExtension);
            }
        }
    }
}
