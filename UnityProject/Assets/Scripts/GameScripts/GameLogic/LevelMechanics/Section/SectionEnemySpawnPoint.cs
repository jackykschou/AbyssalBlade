using System.Collections;
using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.Components.Input;
using Assets.Scripts.GameScripts.GameLogic.Spawner;
using Assets.Scripts.Managers;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.LevelMechanics.Section
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(PrefabSpawner))]
    [AddComponentMenu("LevelMechanics/Section/SectionEnemySpawnPoint")]
    public class SectionEnemySpawnPoint : SectionLogic 
    {
        public PrefabSpawner PrefabSpawner;

        [Range(0f, 100f)] 
        public float SpawnRadius = 0f;

        public bool FadeInEnemy = false;

        [Range(0f, 5f)]
        public float FadeInTime = 0f;


        public FixTimeDispatcher SpawnCoolDown;

        public Collider2D TriggerArea;

        public bool Activated = true;

        private bool _triggered;
        private bool _deactivated;

        public bool CanSpawn 
        {
            get { return _triggered && PrefabSpawner.CanSpawn() && SpawnCoolDown.CanDispatch() && Activated && SectionActivated && GameManager.Instance.PlayerMainCharacter != null && !GameManager.Instance.PlayerMainCharacter.HitPointAtZero(); }
        }

        public override void OnSectionActivated(int sectionId)
        {
            base.OnSectionActivated(sectionId);
            if (sectionId == SectionId)
            {
                TriggerGameEvent(GameEvent.OnSectionEnemySpawnPointActivated, gameObject, SectionId);
                TriggerArea.enabled = true;
                _deactivated = false;
            }
        }

        public override void OnSectionDeactivated(int sectionId)
        {
            base.OnSectionDeactivated(sectionId);
            if (sectionId == SectionId && !_deactivated)
            {
                TriggerGameEvent(GameEvent.OnSectionEnemySpawnPointDeactivated, gameObject, SectionId);
                TriggerArea.enabled = false;
                _deactivated = true;
            }
        }

        protected override void OnTriggerStay2D(Collider2D coll)
        {
            _triggered = true;
        }

        protected override void Update()
        {
            base.Update();
            if (_deactivated)
            {
                return;
            }
            if (SectionActivated && (!Activated || !PrefabSpawner.CanSpawn()))
            {
                TriggerGameEvent(GameEvent.OnSectionEnemySpawnPointDeactivated, gameObject, SectionId);
                TriggerArea.enabled = false;
                _deactivated = true;
                return;
            }
            if (_triggered)
            {
                SpawnEnemy();
            }
        }

        public void SpawnEnemy()
        {
            if (!CanSpawn)
            {
                return;
            }
            SpawnCoolDown.Dispatch();
            Vector3 spawnPosition = new Vector3(Random.Range(transform.position.x - SpawnRadius, transform.position.x + SpawnRadius),
                Random.Range(transform.position.y - SpawnRadius, transform.position.y + SpawnRadius), transform.position.z);
            PrefabSpawner.SpawnPrefab(spawnPosition, o =>
            {
                var triggerNoHitPointOnSectionDeactivated = o.GetComponent<TriggerNoHitPointOnSectionDeactivated>() ??
                                                            o.AddComponent<TriggerNoHitPointOnSectionDeactivated>();
                var triggerOnSectionEnemyDespawnedOnNoHitPoint = o.GetComponent<TriggerOnSectionEnemyDespawnedOnNoHitPoint>() ??
                                                                 o.AddComponent<TriggerOnSectionEnemyDespawnedOnNoHitPoint>();
                triggerNoHitPointOnSectionDeactivated.SectionId = SectionId;
                triggerOnSectionEnemyDespawnedOnNoHitPoint.SectionId = SectionId;
                if(FadeInEnemy)
                    StartCoroutine(FadeInEnemyIE(o, FadeInTime));
            });
            TriggerGameEvent(GameEvent.OnSectionEnemySpawned, SectionId);
        }

        public IEnumerator FadeInEnemyIE(GameObject o, float time)
        {
            float timePass = 0.0f;
            SpriteRenderer render = o.GetComponent<SpriteRenderer>();
            render.color = new Color(render.color.r,render.color.g,render.color.b, 0.0f);
            while (timePass < time)
            {
                render.color = new Color(render.color.r, render.color.g, render.color.b, timePass/time);
                yield return new WaitForSeconds(Time.deltaTime);
                timePass += Time.deltaTime;
            }
            render.color = new Color(render.color.r, render.color.g, render.color.b, 1.0f);
        }

        protected override void Initialize()
        {
            base.Initialize();
            if (PrefabSpawner == null)
            {
                PrefabSpawner = GetComponent<PrefabSpawner>();
            }
            TriggerArea = GetComponent<Collider2D>();
            TriggerArea.isTrigger = true;
            TriggerArea.enabled = false;
            gameObject.layer = LayerConstants.LayerMask.SpawnArea;
            Activated = true;
            _triggered = false;
            _deactivated = false;
        }

        protected override void Deinitialize()
        {
        }
    }
}
