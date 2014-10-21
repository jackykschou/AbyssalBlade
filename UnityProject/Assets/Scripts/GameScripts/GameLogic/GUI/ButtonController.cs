using System.Collections.Generic;
using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.Components.Input;
using Assets.Scripts.Managers;
using UnityEngine;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.GUI
{
    public class ButtonController : GameLogic
    {
        public Color HighlightColor;
        public float BtnScaleAmount;
        public Transform StartButton;
        public Transform OptionsButton;
        public Transform QuitButton;
        public Prefab StartLevelPrefab;
        public ClipName ButtonPressClip;

        private List<Transform> _buttonObjs;
        private Vector3 _popoutAmount;
        private List<bool> _popped;
        private const float RotateAmount = 30.0f;
        private int _numButtons = 0;
        private int _curButton = 0;
        [SerializeField]
        private AxisOnHold VerticalAxis;
        [SerializeField] 
        private AxisOnHold JoystickVerticalAxis;
        [SerializeField]
        private ButtonOnPressed Attack1;
        

        protected override void Initialize()
        {
            base.Initialize();
            _buttonObjs = new List<Transform>()
            {
                StartButton,
                OptionsButton,
                QuitButton
            };
            _popped = new List<bool>()
            {
                false,
                false,
                false
            };
            _popoutAmount = new Vector3(0, 0, -.75f);
            _numButtons = _buttonObjs.Count;
            _curButton = -1;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            if (JoystickVerticalAxis.Detect())
            {
                bool up =
                    Mathf.Abs(VerticalAxis.GetAxisValue()) > Mathf.Abs(JoystickVerticalAxis.GetAxisValue())
                        ? VerticalAxis.GetAxisValue() > 0
                        : JoystickVerticalAxis.GetAxisValue() > 0;

                TriggerGameScriptEvent(GameScriptEvent.ButtonChange, GetNextButton(up));
            }
            else
            {
                RaycastHit hit;
                bool clicked = Input.GetMouseButtonDown(0);
                if (Physics.Raycast(UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    GameObject hitObj = hit.collider.gameObject;
                    for (int index = 0; index < _buttonObjs.Count; index++)
                    {
                        if (hitObj == _buttonObjs[index].gameObject)
                        {
                            if (clicked)
                            {
                                switch (index)
                                {
                                    case 0:
                                        TriggerGameScriptEvent(GameScriptEvent.MenuStartButtonPressed);
                                        break;
                                    case 1:
                                        TriggerGameScriptEvent(GameScriptEvent.MenuOptionsButtonPressed);
                                        break;
                                    case 2:
                                        TriggerGameScriptEvent(GameScriptEvent.MenuQuitButtonPressed);
                                        break;
                                }
                            }
                            if (!_popped[index])
                                TriggerGameScriptEvent(GameScriptEvent.OnButtonMouseOver, hitObj, index);
                            return;
                        }
                    }
                    for (int index = 0; index < _buttonObjs.Count; index++)
                        if (_popped[index])
                            TriggerGameScriptEvent(GameScriptEvent.OnButtonMouseLeave, index);
                }
                return;
            }

            if (Attack1.Detect())
            {
                switch (_curButton)
                {
                    case 0:
                        TriggerGameScriptEvent(GameScriptEvent.MenuStartButtonPressed);
                        break;
                    case 1:
                        TriggerGameScriptEvent(GameScriptEvent.MenuOptionsButtonPressed);
                        break;
                    case 2:
                        TriggerGameScriptEvent(GameScriptEvent.MenuQuitButtonPressed);
                        break;
                }
            }
        }

        private int GetNextButton(bool up)
        {
            if (up)
            {
                _curButton--;
                if (_curButton == -1)
                    _curButton = _numButtons - 1;
            }
            else
            {
                _curButton++;
                if (_curButton == _numButtons)
                    _curButton = 0;
            }
            return _curButton;
        }

        [GameScriptEventAttribute(GameScriptEvent.ButtonChange)]
        public void ButtonChange(int buttonId)
        {
            switch (buttonId)
            {
                case 0:
                    TriggerGameScriptEvent(GameScriptEvent.OnButtonMouseOver, StartButton.gameObject, 0);
                    TriggerGameScriptEvent(GameScriptEvent.OnButtonMouseLeave, 1);
                    TriggerGameScriptEvent(GameScriptEvent.OnButtonMouseLeave, 2);
                    break;
                case 1:
                    TriggerGameScriptEvent(GameScriptEvent.OnButtonMouseOver, OptionsButton.gameObject, 1);
                    TriggerGameScriptEvent(GameScriptEvent.OnButtonMouseLeave, 0);
                    TriggerGameScriptEvent(GameScriptEvent.OnButtonMouseLeave, 2);
                    break;
                case 2:
                    TriggerGameScriptEvent(GameScriptEvent.OnButtonMouseOver, QuitButton.gameObject, 2);
                    TriggerGameScriptEvent(GameScriptEvent.OnButtonMouseLeave, 0);
                    TriggerGameScriptEvent(GameScriptEvent.OnButtonMouseLeave, 1);
                    break;
            }
        }
        [GameScriptEventAttribute(GameScriptEvent.MenuStartButtonPressed)]
        public void OnStartPressed()
        {
            AudioManager.Instance.PlayClipImmediate(ButtonPressClip);
            GameManager.Instance.ChangeLevel(StartLevelPrefab);
        }

        [GameScriptEventAttribute(GameScriptEvent.MenuOptionsButtonPressed)]
        public void OnOptionsPressed()
        {
            AudioManager.Instance.PlayClipImmediate(ButtonPressClip);
        }

        [GameScriptEventAttribute(GameScriptEvent.MenuQuitButtonPressed)]
        public void OnQuitPressed()
        {
            AudioManager.Instance.PlayClipImmediate(ButtonPressClip);
            Application.Quit();
        }

        [GameScriptEventAttribute(GameScriptEvent.OnButtonMouseOver)]
        public void OnButtonMouseOver(GameObject hitObj, int index)
        {
            if (_popped[index])
                return;
            _popped[index] = true;
            hitObj.GetComponentInChildren<ParticleSystem>().Play();
            hitObj.transform.Rotate(Vector3.up, RotateAmount);
            hitObj.transform.localScale *= BtnScaleAmount;
            hitObj.transform.Translate(_popoutAmount);
            hitObj.renderer.material.SetColor("_OutlineColor", HighlightColor);
        }

        [GameScriptEventAttribute(GameScriptEvent.OnButtonMouseLeave)]
        public void OnButtonMouseLeave(int index)
        {
            if (!_popped[index])
                return;
            _popped[index] = false;
            _buttonObjs[index].GetComponentInChildren<ParticleSystem>().Stop();
            _buttonObjs[index].Translate(-_popoutAmount);
            _buttonObjs[index].localScale /= BtnScaleAmount;
            _buttonObjs[index].Rotate(Vector3.up, -RotateAmount);
            _buttonObjs[index].renderer.material.SetColor("_OutlineColor", Color.black);
        }
        protected override void Deinitialize()
        {
        }
    }
}