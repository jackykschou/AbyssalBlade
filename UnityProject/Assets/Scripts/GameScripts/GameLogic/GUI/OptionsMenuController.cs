﻿using System.Collections.Generic;
using Assets.Scripts.Attributes;
using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.Input;
using Assets.Scripts.Managers;
using StateMachine.Action.UNavMeshAgent;
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
        [SerializeField] 
        private ButtonOnPressed SubmitButton;

        private EventSystem _eventSystem;
        private Selectable _resumeButton;
        private Selectable _menuButton;
        private Selectable _quitButton;
        private GameObject _optionsBG;
        private int _curSelectedIndex;
        private List<Selectable>_buttons;
        private bool _paused;

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
            _paused = false;
            HideMenu();
        }

        protected override void Update()
        {
            base.Update();

            // Handle Pause Button
            if (OptionsButton.Detect())
            {
                _paused = !_paused;
                if (_paused)
                    ShowMenu();
                else
                    HideMenu();
            }

            if (!_paused) 
                return;


            // Handle Submit
            if (SubmitButton.Detect())
            {
                Selectable selected = _eventSystem.currentSelectedGameObject.GetComponent<Selectable>();
                if (selected == _resumeButton)
                {
                    OnResumePressed();
                }
                else if (selected == _menuButton)
                {
                    OnMenuPressed();
                }
                else if (selected == _quitButton)
                {
                    OnQuitPressed();
                }
            }

            // Handle Joystick
            if (AxisOnHold.Detect())
            {
                if (AxisOnHold.GetAxisValue() > 0.0f)
                    GoDown();
                else
                    GoUp();
                SelectButton();
            }
        }

        private void ShowMenu()
        {
            TriggerGameEvent(Constants.GameEvent.DisablePlayerCharacter);
            _curSelectedIndex = 0;
            _optionsBG.SetActive(true);
            _eventSystem.SetSelectedGameObject(null, new BaseEventData(_eventSystem));
            _eventSystem.SetSelectedGameObject(_resumeButton.gameObject, new BaseEventData(_eventSystem));
        }
        private void HideMenu()
        {
            _optionsBG.SetActive(false);
            TriggerGameEvent(Constants.GameEvent.EnablePlayerCharacter);
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
            _curSelectedIndex = 0;
            _paused = false;
            SelectButton();
        }
    }
}