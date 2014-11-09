using System.Collections;
using Assets.Scripts.Attributes;
using Assets.Scripts.GameScripts.GameLogic.Input;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameScripts.GameLogic.Misc
{
    [RequireComponent(typeof(ButtonOnPressed))]
    public class LoadingScreen : GameLogic
    {
        public Text LoadingText;
        public ButtonOnPressed ButtonOnPressed;

        [GameScriptEvent(Constants.GameScriptEvent.LoadingScreenStartLoading)]
        public void LoadingScreenStartLoading()
        {
            LoadingText.text = "Loading...";
        }

        [GameEvent(Constants.GameEvent.OnLevelFinishedLoading)]
        public void LoadingScreenFinishLoading()
        {
            LoadingText.text = "Press To Continue";
            StartCoroutine(WaitForPress());
        }

        public IEnumerator WaitForPress()
        {
            while (!ButtonOnPressed.Detect())
            {
                yield return new WaitForSeconds(Time.deltaTime);
            }
            TriggerGameEvent(Constants.GameEvent.OnLoadingScreenFinished);
        }

        protected override void Deinitialize()
        {
        }
    }
}
