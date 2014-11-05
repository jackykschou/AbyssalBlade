using System.Collections.Generic;
using Assets.Scripts.Attributes;
using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.Input;
using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.GameScripts.GameLogic.GUI
{
    public class OptionsMenuController : GameLogic
    {
        [SerializeField]
        private AxisOnHold AxisOnHold;
        [SerializeField]
        private ButtonOnPressed OptionsButton;

        private EventSystem _eventSystem;
        private Selectable _resumeButton;
        private Selectable _menuButton;
        private Selectable _quitButton;
        private GameObject _optionsBG;
        private int _curSelectedIndex;
        private List<Selectable>_buttons;
        private bool _paused = false;

        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            _eventSystem = GetComponent<EventSystem>();
            _optionsBG = GameObject.Find("OptionsBG");
            _buttons = new List<Selectable>();
            _resumeButton = GameObject.Find("ResumeButtonOptions").GetComponent<Selectable>();
            _buttons.Add(_resumeButton);
            _menuButton = GameObject.Find("MenuButtonOptions").GetComponent<Selectable>();
            _buttons.Add(_menuButton);
            _quitButton = GameObject.Find("QuitButtonOptions").GetComponent<Selectable>();
            _buttons.Add(_quitButton);
            _curSelectedIndex = 0;
            HideMenu();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            if (OptionsButton.Detect())
            {
                _paused = !_paused;
                if (_paused)
                {
                    ShowMenu();
                    _curSelectedIndex = 2;
                    SelectButton();
                    _curSelectedIndex = 0;
                    SelectButton();
                }
                else
                {
                    HideMenu();
                }
            }

            if (_paused)
            {
                if (AxisOnHold.Detect())
                {
                    float v = AxisOnHold.GetAxisValue();
                    if (v > 0.0f)
                    {
                        GoDown();
                    }
                    else if (v < 0.0f)
                    {
                        GoUp();
                    }
                    SelectButton();
                }
            }
        }

        private void ShowMenu()
        {
            TriggerGameEvent(Constants.GameEvent.DisablePlayerCharacter);
            _optionsBG.SetActive(true);
            SelectButton();
            //_paused = true;
        }
        private void HideMenu()
        {
            SelectButton();
            _optionsBG.SetActive(false);
            TriggerGameEvent(Constants.GameEvent.EnablePlayerCharacter);
            //_paused = false;
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
            _paused = false;
            HideMenu();
            GameManager.Instance.ChangeLevel(Prefab.MainMenu);
        }

        public void OnQuitPressed()
        {
            Application.Quit();
        }

        protected override void Deinitialize()
        {

        }
    }
}