using System.Collections.Generic;
using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.Input;
using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;
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
        private AxisOnHold AxisOnHold;
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
            _curButton = 0;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            OnButtonMouseOver(_curButton);
            if (AxisOnHold.Detect())
            {
                ButtonChange(GetNextButton(AxisOnHold.GetAxisValue() > 0.01f));
            }
            else
            {
                RaycastHit hit;
                bool clicked = UnityEngine.Input.GetMouseButtonDown(0);
                if (Physics.Raycast(UnityEngine.Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition), out hit, LayerConstants.LayerMask.StaticObstacle))
                {
                    GameObject hitObj = hit.collider.gameObject;
                    for (int index = 0; index < _buttonObjs.Count; index++)
                    {
                        if (hitObj == _buttonObjs[index].gameObject)
                        {
                            _curButton = index;
                            ButtonChange(_curButton);
                            if (clicked)
                            {
                                switch (index)
                                {
                                    case 0:
                                        OnStartPressed();
                                        break;
                                    case 1:
                                        OnOptionsPressed();
                                        break;
                                    case 2:
                                        OnQuitPressed();
                                        break;
                                }
                            }
                        }
                    }
                }
            }

            if (Attack1.Detect())
            {
                switch (_curButton)
                {
                    case 0:
                        OnStartPressed();
                        break;
                    case 1:
                        OnOptionsPressed();
                        break;
                    case 2:
                        OnQuitPressed();
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

        public void ButtonChange(int buttonId)
        {
            switch (buttonId)
            {
                case 0:
                    OnButtonMouseOver(0);
                    OnButtonMouseLeave(1);
                    OnButtonMouseLeave(2);
                    break;
                case 1:
                    OnButtonMouseOver(1);
                    OnButtonMouseLeave(0);
                    OnButtonMouseLeave(2);
                    break;
                case 2:
                    OnButtonMouseOver(2);
                    OnButtonMouseLeave(0);
                    OnButtonMouseLeave(1);
                    break;
            }
            _curButton = buttonId;
        }

        public void OnStartPressed()
        {
            OnButtonMouseLeave(0);
            OnButtonMouseLeave(1);
            OnButtonMouseLeave(2);
            AudioManager.Instance.PlayClipImmediate(ButtonPressClip);
            GameManager.Instance.ChangeLevel(StartLevelPrefab);
        }

        public void OnOptionsPressed()
        {
            AudioManager.Instance.PlayClipImmediate(ButtonPressClip);
        }

        public void OnQuitPressed()
        {
            OnButtonMouseLeave(0);
            OnButtonMouseLeave(1);
            OnButtonMouseLeave(2);
            AudioManager.Instance.PlayClipImmediate(ButtonPressClip);
            Application.Quit();
        }

        public void OnButtonMouseOver(int index)
        {
            GameObject hitObj = _buttonObjs[_curButton].gameObject;
            if (_popped[index])
                return;
            _popped[index] = true;
            hitObj.GetComponentInChildren<ParticleSystem>().Play();
            hitObj.transform.Rotate(Vector3.up, RotateAmount);
            hitObj.transform.localScale *= BtnScaleAmount;
            hitObj.transform.Translate(_popoutAmount);
            hitObj.renderer.material.SetColor("_OutlineColor", HighlightColor);
            _curButton = index;
            for(int i = 0; i < _numButtons; i++)
                if(i != index)
                    OnButtonMouseLeave(i);
        }

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