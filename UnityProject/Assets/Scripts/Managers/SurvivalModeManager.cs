using System.Collections.Generic;
using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.Spawner;
using Assets.Scripts.Utility;
using UnityEngine;
using Assets.Scripts.GameScripts.GameLogic;
using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.Managers
{
    public class SurvivalModeManager : GameLogic
    {
        public List<Prefab> SpawnPoints;
        public List<Transform> AreaSpawnPoints;

        private int _nextSpawnAreaIndex;
        private Prefab _currentAreaPrefab;
        private GameObject _currentArea;

        public static SurvivalModeManager Instance
        {
            get { return _instance ?? (_instance = FindObjectOfType<SurvivalModeManager>()); }
        }
        private static SurvivalModeManager _instance;

        private Vector3 NextSpawnPosition()
        {
            Vector3 nextPoint = AreaSpawnPoints[_nextSpawnAreaIndex++].position;
            _nextSpawnAreaIndex %= AreaSpawnPoints.Count;
            return nextPoint;
        }

        public Prefab GetNextArea()
        {
            Prefab nextArea = SpawnPoints[Random.Range(0, SpawnPoints.Count-1)];
            while (nextArea == _currentAreaPrefab)
            {
                nextArea = SpawnPoints[Random.Range(0, SpawnPoints.Count - 1)];
            }
            return nextArea;
        }

        [GameEventAttribute(GameEvent.SurvivalSectionEnded)]
        public void SpawnNextSection()
        {
            TriggerGameEvent(GameEvent.DisablePlayerCharacter);
            if (_currentArea != null)
            {
                PrefabManager.Instance.DespawnPrefab(_currentArea);
            }

            _currentAreaPrefab = GetNextArea();

            PrefabManager.Instance.SpawnPrefabImmediate(Prefab.SpawnParticleSystem, GameManager.Instance.PlayerMainCharacter.transform.position, o =>
            {
                o.transform.parent = GameManager.Instance.PlayerMainCharacter.transform;
                o.GetComponent<ParticleSystem>().Play();
            });
            PrefabManager.Instance.SpawnPrefabImmediate(_currentAreaPrefab, NextSpawnPosition(), o =>
            {
                _currentArea = o;
            });
            _currentArea.TriggerGameScriptEvent(GameScriptEvent.SurvivalAreaSpawned);
            TriggerGameEvent(GameEvent.SurvivalSectionStarted);
            if (AstarPath.active != null)
            {
                AstarPath.active.Scan();
            }
            GameManager.Instance.PlayerMainCharacter.transform.position = new Vector3(_currentArea.GetComponentInChildren<PlayerSpawnPoint>().transform.position.x,
                                                                                       _currentArea.GetComponentInChildren<PlayerSpawnPoint>().transform.position.y,
                                                                                       GameManager.Instance.PlayerMainCharacter.transform.position.z);
            GameManager.Instance.MainCamera.transform.position = new Vector3(_currentArea.GetComponentInChildren<PlayerSpawnPoint>().transform.position.x,
                                                                             _currentArea.GetComponentInChildren<PlayerSpawnPoint>().transform.position.y,
                                                                             GameManager.Instance.MainCamera.transform.position.z);
            TriggerGameEvent(GameEvent.EnablePlayerCharacter);
        }

        [GameEventAttribute(GameEvent.OnLevelStarted)]
        public void OnLevelStarted()
        {
            SpawnNextSection();
        }

        protected override void Initialize()
        {
            base.Initialize();
            _currentAreaPrefab = Prefab.None;
            _currentArea = null;
        }

        protected override void Deinitialize()
        {
        }
    }
}