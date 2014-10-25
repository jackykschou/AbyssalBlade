using System;
using System.Collections;
using System.Reflection;
using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic;
using Assets.Scripts.Managers;
using Assets.Scripts.Utility;
using UnityEngine;

using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.GameScripts
{
    [RequireComponent(typeof(GameScriptEditorUpdate))]
    [RequireComponent(typeof(GameScriptEventManager))]
    public abstract class GameScript : MonoBehaviour
    {
        public string LabelName;
        public GameScriptEventManager GameScriptEventManager { get; set; }

        private bool _firstTimeInitialized = false;

        public bool Initialized {
            get { return _initialized; }
        }
        private bool _initialized = false;
        public bool Deinitialized
        {
            get { return _deinitialized; }
        }
        private bool _deinitialized = false;
        public bool Disabled
        {
            get { return _disabled; }
        }
        private bool _disabled = false;

        protected virtual void FirstTimeInitialize()
        {
        }

        protected abstract void Initialize();

        protected abstract void Deinitialize();

        public virtual void EditorUpdate()
        {
        }

        public void TriggerGameScriptEvent(GameScriptEvent gameScriptEvent, params object[] args)
        {
            StartCoroutine(TriggerGameScriptEventIE(gameScriptEvent, args));
        }

        IEnumerator TriggerGameScriptEventIE(GameScriptEvent gameScriptEvent, params object[] args)
        {
            while (GameScriptEventManager == null || !GameScriptEventManager.Initialized)
            {
                yield return new WaitForSeconds(Time.deltaTime);
            }

            GameScriptEventManager.TriggerGameScriptEvent(gameScriptEvent, args);

            foreach (Transform t in transform)
            {
                foreach (var s in t.gameObject.GetComponents<GameScript>())
                {
                    s.TriggerGameScriptEvent(s, gameScriptEvent, args);
                }
            }
        }

        public void TriggerGameScriptEvent<T>(GameScriptEvent gameScriptEvent, params object[] args) where T : GameScript
        {
            StartCoroutine(TriggerGameScriptEventIE(gameScriptEvent, args));
        }

        public IEnumerator TriggerGameScriptEventIE<T>(GameScriptEvent gameScriptEvent, params object[] args) where T : GameScript
        {
            while (GameScriptEventManager == null || !GameScriptEventManager.Initialized)
            {
                yield return new WaitForSeconds(Time.deltaTime);
            }

            GameScriptEventManager.TriggerGameScriptEvent<T>(gameScriptEvent, args);

            foreach (Transform t in transform)
            {
                foreach (var s in t.gameObject.GetComponents<GameScript>())
                {
                    if (s is T)
                    {
                        s.TriggerGameScriptEvent(s, gameScriptEvent, args);
                    }
                }
            }
        }

        public void TriggerGameScriptEvent(GameScript gameScript, GameScriptEvent gameScriptEvent, params object[] args)
        {
            StartCoroutine(TriggerGameScriptEventIE(gameScript, gameScriptEvent, args));
        }

        public IEnumerator TriggerGameScriptEventIE(GameScript gameScript, GameScriptEvent gameScriptEvent, params object[] args)
        {
            while (GameScriptEventManager == null || !GameScriptEventManager.Initialized)
            {
                yield return new WaitForSeconds(Time.deltaTime);
            }

            GameScriptEventManager.TriggerGameScriptEvent(gameScript, gameScriptEvent, args);

            foreach (Transform t in transform)
            {
                foreach (var s in t.gameObject.GetComponents<GameScript>())
                {
                    s.TriggerGameScriptEvent(s, gameScriptEvent, args);
                }
            }
        }

        public void TriggerGameEvent(GameScript gameScript, GameEvent gameEvent, params System.Object[] args)
        {
            GameEventManager.Instance.TriggerGameEvent(gameScript, gameEvent, args);
        }

        public void TriggerGameEvent(GameEvent gameEvent, params System.Object[] args)
        {
            GameEventManager.Instance.TriggerGameEvent(gameEvent, args);
        }

        void Start()
        {
            InitializeHelper();
        }

        void OnEnable()
        {
            InitializeHelper();
        }

        void OnSpawned()
        {
            InitializeHelper();
        }

        void InitializeHelper()
        {

            if (_initialized)
            {
                return;
            }
            if (!_firstTimeInitialized)
            {
                InitializeFields();
                SubscribeGameEvents();
                FirstTimeInitialize();
            }

            Initialize();

            if (!_firstTimeInitialized)
            {
                gameObject.CacheGameObject();
                _firstTimeInitialized = true;
            }
            _deinitialized = false;
            _initialized = true;
            _disabled = false;
            GameScriptEventManager.UpdateInitialized();
        }

        protected virtual void Update()
        {
        }

        protected virtual void FixedUpdate()
        {
        }

        public void DisableGameObject(float delay = 0f)
        {
            if (!gameObject.activeSelf || _disabled)
            {
                return;
            }

            _disabled = true;
            StartCoroutine(DisableGameObjectIE(delay));
        }

        IEnumerator DisableGameObjectIE(float delay)
        {
            yield return new WaitForEndOfFrame();
            if (delay > 0f)
            {
                yield return new WaitForSeconds(delay);
            }
            TriggerGameScriptEvent(GameScriptEvent.OnObjectDestroyed);
            if (PrefabManager.Instance.IsSpawnedFromPrefab(gameObject))
            {
                PrefabManager.Instance.DespawnPrefab(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void ImmediateDisableGameObject()
        {
            if (!gameObject.activeSelf || _disabled)
            {
                return;
            }

            _disabled = true;
            TriggerGameScriptEvent(GameScriptEvent.OnObjectDestroyed);
            if (PrefabManager.Instance.IsSpawnedFromPrefab(gameObject))
            {
                PrefabManager.Instance.DespawnPrefab(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void OnDespawned()
        {
            DeinitializeHelper();
        }

        void OnDisable()
        {
            DeinitializeHelper();
        }

        void OnDestroy()
        {
            DeinitializeHelper();
        }

        void DeinitializeHelper()
        {
            if (_deinitialized || !_initialized)
            {
                return;
            }

            if (_disabled && !PrefabManager.Instance.IsSpawnedFromPrefab(gameObject))
            {
                UnsubscribeGameEvents();
                gameObject.UncacheGameObject();
            }

            Deinitialize();
            _initialized = false;
            _deinitialized = true;
        }

        private void InitializeFields()
        {
            GameScriptEventManager = GetComponent<GameScriptEventManager>();
            GameScriptEventManager.UpdateGameScriptEvents(this);
        }
	
        private void SubscribeGameEvents()
        {
            foreach (var info in GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance))
            {
                foreach (var attr in Attribute.GetCustomAttributes(info))
                {
                    if (attr.GetType() == typeof(GameEventAttribute))
                    {
                        GameEventAttribute gameEventSubscriberAttribute = attr as GameEventAttribute;
                        GameEventManager.Instance.SubscribeGameEvent(this, gameEventSubscriberAttribute.Event, info);
                    }
                }
            }
        }

        private void UnsubscribeGameEvents()
        {
            foreach (var info in GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance))
            {
                foreach (var attr in Attribute.GetCustomAttributes(info))
                {
                    if (attr.GetType() == typeof(GameEventAttribute))
                    {
                        GameEventAttribute gameEventSubscriberAttribute = attr as GameEventAttribute;
                        GameEventManager.Instance.UnsubscribeGameEvent(this, gameEventSubscriberAttribute.Event);
                    }
                }
            }
        }

        protected virtual void OnTriggerEnter2D(Collider2D coll)
        { }

        protected virtual void OnTriggerStay2D(Collider2D coll)
        { }

        protected virtual void OnTriggerExit2D(Collider2D coll)
        { }

        protected virtual void OnCollisionEnter2D(Collision2D coll)
        { }

        protected virtual void OnCollisionStay2D(Collision2D coll)
        { }

        protected virtual void OnCollisionExit2D(Collision2D coll)
        { }
    }
}
