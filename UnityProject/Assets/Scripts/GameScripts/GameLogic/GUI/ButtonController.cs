using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Constants;
using UnityEngine;
using UnityEngine.UI;

using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.GUI
{
    public class ButtonController : GameLogic
    {
        public Color highlightColor;

        public float btnScaleAmount;
        public Transform StartButton;
        public Transform OptionsButton;
        public Transform QuitButton;
        List<Transform> _buttonObjs;
        Vector3 popoutAmount;
        List<bool> popped;


        private float rotateAmount = 30.0f;
        

        protected override void Initialize()
        {
            base.Initialize();
            _buttonObjs = new List<Transform>()
            {
                StartButton,
                OptionsButton,
                QuitButton
            };
            popped = new List<bool>()
            {
                false,
                false,
                false
            };
            popoutAmount = new Vector3(0, 0, -.75f);
        }

        protected override void Update()
        {
            base.Update();

            RaycastHit hit;
            bool clicked = Input.GetMouseButtonDown(0) || Input.GetButtonDown(InputConstants.GetKeyCodeName(InputKeyCode.Attack1));

            
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
                        if (!popped[index])
                            TriggerGameScriptEvent(GameScriptEvent.OnButtonMouseOver, hitObj, index);
                        return;
                    }
                }
                for (int index = 0; index < _buttonObjs.Count; index++)
                    if (popped[index])
                        TriggerGameScriptEvent(GameScriptEvent.OnButtonMouseLeave, index);
            }

            return;
        }

        [GameScriptEventAttribute(GameScriptEvent.MenuStartButtonPressed)]
        public void onStartPressed()
        {
            Debug.Log("Start Pressed");
        }

        [GameScriptEventAttribute(GameScriptEvent.MenuOptionsButtonPressed)]
        public void onOptionsPressed()
        {
            Debug.Log("Options Pressed");
        }

        [GameScriptEventAttribute(GameScriptEvent.MenuQuitButtonPressed)]
        public void onQuitPressed()
        {
            Debug.Log("Quit Pressed");
        }

        [GameScriptEventAttribute(GameScriptEvent.OnButtonMouseOver)]
        public void onButtonMouseOver(GameObject hitObj, int index)
        {
            popped[index] = true;
            hitObj.GetComponentInChildren<ParticleSystem>().Play();
            hitObj.transform.Rotate(Vector3.up, rotateAmount);
            hitObj.transform.localScale *= btnScaleAmount;
            hitObj.transform.Translate(popoutAmount);
            hitObj.renderer.material.SetColor("_OutlineColor", highlightColor);
        }
        [GameScriptEventAttribute(GameScriptEvent.OnButtonMouseLeave)]
        public void onButtonMouseLeave(int index)
        {
            popped[index] = false;
            _buttonObjs[index].GetComponentInChildren<ParticleSystem>().Stop();
            _buttonObjs[index].Translate(-popoutAmount);
            _buttonObjs[index].localScale /= btnScaleAmount;
            _buttonObjs[index].Rotate(Vector3.up, -rotateAmount);
            _buttonObjs[index].renderer.material.SetColor("_OutlineColor", Color.black);
        }
        protected override void Deinitialize()
        {
        }
    }
}