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
            LoadingText.color = new Color(LoadingText.color.r, LoadingText.color.g, LoadingText.color.b, 1);
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
                LoadingText.color = new Color(LoadingText.color.r, LoadingText.color.g, LoadingText.color.b, Mathf.PingPong(Time.time, 1));
            }
            TriggerGameEvent(Constants.GameEvent.OnLoadingScreenFinished);
        }

        protected override void Deinitialize()
        {
        }
    }
}
