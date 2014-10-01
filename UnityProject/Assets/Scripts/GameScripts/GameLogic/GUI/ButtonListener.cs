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
    public class ButtonListener : GameLogic
    {
        public Transform StartButton;
        public Transform OptionsButton;
        public Transform QuitButton;
        List<ParticleSystem> _buttonParticles;
        List<Transform> _buttonObjs;
        static int curButton = -1;
        Vector3 popoutAmount;
        List<bool> popped;
        

        protected override void Initialize()
        {
            base.Initialize();
            _buttonParticles = new List<ParticleSystem>()
            { 
                StartButton.parent.GetComponentInChildren<ParticleSystem>(),
                OptionsButton.parent.GetComponentInChildren<ParticleSystem>(),
                QuitButton.parent.GetComponentInChildren<ParticleSystem>()
            };
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

        void Update()
        {
            RaycastHit hit;
            bool clicked = Input.GetMouseButtonDown(0) || Input.GetButtonDown(InputConstants.GetKeyCodeName(InputKeyCode.Attack1));

            
            if (Physics.Raycast(UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                GameObject hitObj = hit.collider.gameObject;
                int btn = hitObj == StartButton.gameObject ? 0 : (hitObj == OptionsButton.gameObject ? 1 : (hitObj == QuitButton.gameObject ? 2 : -1));
                if (hitObj == StartButton.gameObject)
                {
                    curButton = 0;
                    if(clicked)
                        TriggerGameScriptEvent(GameScriptEvent.MenuStartButtonPressed);
                    if (!popped[curButton])
                    {
                        popped[0] = true;
                        _buttonParticles[0].Play();
                        hitObj.transform.Translate(popoutAmount);
                    }
                    return;
                }
                if (hitObj == OptionsButton.gameObject)
                {
                    curButton = 1;
                    if (clicked)
                        TriggerGameScriptEvent(GameScriptEvent.MenuOptionsButtonPressed);
                    if (!popped[curButton])
                    {
                        popped[1] = true;
                        _buttonParticles[1].Play();
                        hitObj.transform.Translate(popoutAmount);
                    }
                    return;
                }
                if (hitObj == QuitButton.gameObject)
                {
                    curButton = 2;
                    if (clicked)
                        TriggerGameScriptEvent(GameScriptEvent.MenuQuitButtonPressed);
                    if (!popped[curButton])
                    {
                        popped[2] = true;
                        _buttonParticles[2].Play();
                        hitObj.transform.Translate(popoutAmount);
                    }
                    return;
                }

                if (curButton != -1 && popped[curButton])
                {
                    _buttonObjs[curButton].Translate(-popoutAmount);
                    popped[curButton] = false;
                }
                curButton = -1;
            } 
        }

        [GameScriptEventAttribute(GameScriptEvent.MenuStartButtonPressed)]
        public void onStartPressed()
        {
            Debug.Log("Start Pressed");
            // Animate the button
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

        protected override void Deinitialize()
        {
        }
    }
}