using System;
using System.Collections;
using Assets.Scripts.Constants;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GameScripts.GameLogic;
using Assets.Scripts.Utility;
using Assets.Scripts.GameScripts.GameLogic.Spawner;
using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;
using Assets.Scripts.GameScripts.GameLogic.ObjectMotor;

namespace Assets.Scripts.Managers
{
    [AddComponentMenu("Manager/MessageManager")]
    [ExecuteInEditMode]
    public class MessageManager : GameLogic
    {
        public PrefabSpawner PrefabSpawner;
        public Camera MainCamera;
        public EaseType preferredEaseType;

        private static MessageManager _instance;
        public static MessageManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = (MessageManager)UnityEngine.Object.FindObjectOfType<MessageManager>();
                    DontDestroyOnLoad(_instance.gameObject);
                }
                return _instance;
            }
        }

        public void DisplayMessage(string message,Vector3 direction)
        {
            GameObject prefab = PrefabSpawner.SpawnPrefab(TopMiddleOfScreen());
            TextMesh mesh = prefab.GetComponent<TextMesh>();
            TextMotor motor = prefab.GetComponent<TextMotor>();

            prefab.transform.parent = MainCamera.gameObject.transform;
            mesh.text = message;
            motor.Shoot(preferredEaseType, direction, 5.0f, 1.5f);
        }

        public void DisplayGameMessageFlyAway(string message, Vector3 direction, Vector3 directionToFly, float WhenToFly)
        {
            GameObject prefab = PrefabSpawner.SpawnPrefab(TopMiddleOfScreen());
            TextMesh mesh = prefab.GetComponent<TextMesh>();
            TextMotor motor = prefab.GetComponent<TextMotor>();

            prefab.transform.parent = MainCamera.gameObject.transform;
            mesh.text = message;
            motor.Shoot(preferredEaseType, direction, 5.0f, 1.5f);
            StartCoroutine(this.DelayedMove(motor, directionToFly, WhenToFly));
        }

        public IEnumerator DelayedMove(TextMotor motor, Vector3 direction, float whenToMove)
        {
            yield return new WaitForSeconds(whenToMove);
            motor.Shoot(preferredEaseType, direction, 5.0f, 20.0f);
        }

        Vector3 MiddleOfScreen()
        {
            return MainCamera.ScreenToWorldPoint(new Vector3(MainCamera.pixelWidth / 2.0f, MainCamera.pixelHeight / 2.0f, 0.0f));
        }
        Vector3 TopMiddleOfScreen()
        {
            return MainCamera.ScreenToWorldPoint(new Vector3(MainCamera.pixelWidth / 2.0f, MainCamera.pixelHeight*3.0f / 5.0f, 0.0f));
        }
        Vector3 TopRightMiddleOfScreen()
        {
            return MainCamera.ScreenToWorldPoint(new Vector3(MainCamera.pixelWidth*3.0f / 4.0f, MainCamera.pixelHeight * 3.0f / 5.0f, 0.0f));
        }
        Vector3 TopLeftMiddleOfScreen()
        {
            return MainCamera.ScreenToWorldPoint(new Vector3(MainCamera.pixelWidth / 4.0f, MainCamera.pixelHeight * 3.0f / 5.0f, 0.0f));
        }


        void Awake()
        {
        }

        protected override void Update()
        {
            if (Input.GetKeyDown(KeyCode.Keypad1))
                DisplayMessage("Regular Message", Vector3.up);
            if (Input.GetKeyDown(KeyCode.Keypad2))
                DisplayGameMessageFlyAway("Fly Away", Vector3.up, Vector3.right*10.0f, 2.0f);
        }

        protected override void Initialize()
        {
            if (PrefabSpawner == null)
            {
                PrefabSpawner = GetComponent<PrefabSpawner>();
            }
            if(MainCamera == null)
            {
                MainCamera = Camera.main;
            }
        }

        protected override void Deinitialize()
        {
        }

    }
}