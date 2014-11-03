using System.Collections.Generic;
using Assets.Scripts.Attributes;
using Assets.Scripts.Constants;
using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.GameScripts.GameLogic.GUI
{
    public class OptionsMenuController : GameLogic
    {
        private EventSystem _eventSystem;

        private Selectable _resumeButton;
        private Selectable _menuButton;
        private Selectable _quitButton;
        private GameObject _optionsBG;
        private int _curSelectedIndex;
        private List<Selectable>_buttons;
        private bool _paused = false;

        protected override void Initialize()
        {
            base.Initialize();
            _eventSystem = GetComponent<EventSystem>();
            _optionsBG = GameObject.Find("OptionsBG");
            _curSelectedIndex = 0;
            _buttons = new List<Selectable>();
            _resumeButton = GameObject.Find("ResumeButtonOptions").GetComponent<Selectable>();
            _buttons.Add(_resumeButton);
            _menuButton = GameObject.Find("MenuButtonOptions").GetComponent<Selectable>();
            _buttons.Add(_menuButton);
            _quitButton = GameObject.Find("QuitButtonOptions").GetComponent<Selectable>();
            _buttons.Add(_quitButton);
            SelectButton();
            HideMenu();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
            {
                _paused = !_paused;
                if (_paused)
                {
                    TriggerGameEvent(Constants.GameEvent.DisablePlayerCharacter);
                    ShowMenu();
                }
                else
                {
                    TriggerGameEvent(Constants.GameEvent.EnablePlayerCharacter);
                    HideMenu();
                }
            }

            if (_paused)
            {
                float v = UnityEngine.Input.GetAxis("VerticalAxis");
                if (v > 0.01)
                {
                    GoDown();
                }
                else if (v < -.01)
                {
                    GoUp();
                }
            }
        }

        private void ShowMenu()
        {
            _optionsBG.SetActive(true);
        }
        private void HideMenu()
        {
            _optionsBG.SetActive(false);
        }

        private void SelectButton()
        {
            _eventSystem.SetSelectedGameObject(_buttons[_curSelectedIndex].gameObject, new BaseEventData(_eventSystem));
        }

        private void GoUp()
        {
            if (_curSelectedIndex < _buttons.Count - 1)
                _curSelectedIndex = _curSelectedIndex + 1;
        }

        private void GoDown()
        {
            if (_curSelectedIndex > 0)
                _curSelectedIndex = _curSelectedIndex - 1;
        }

        public void OnResumePressed()
        {
            _paused = false;
            HideMenu();
        }

        public void OnMenuPressed()
        {
            GameManager.Instance.ChangeLevel(Prefab.MainMenu);
        }

        public void OnQuitPressed()
        {
        }

        protected override void Deinitialize()
        {

        }
    }
}