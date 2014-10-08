using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Scripts.Managers;
using Assets.Scripts.Constants;
using Assets.Scripts.Attributes;
using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;
using Assets.Scripts.Utility;
using Assets.Scripts.GameScripts.GameLogic.ObjectMotor;

namespace Assets.Scripts.GameScripts.GameLogic.DamageTextDespawn
{
    public class DamageTextDespawn : GameLogic
    {
        public float origDespawnTime = 1.0f;
        public float scrollingVelocity = 0.5f;
        private float timeAlive;
        private TextMesh mesh;
        private Vector3 Direction;
        private float Speed;
        private float Distance;

        public void OnSpawned()
        {
            timeAlive = 0.0f;
            this.StartCoroutine(this.TimedDespawn());
            TextMotor motor = gameObject.GetComponent<TextMotor>();
            motor.Shoot(Direction,Speed,Distance);
        }

        private IEnumerator TimedDespawn()
        {
            yield return new WaitForSeconds(this.origDespawnTime);
            PrefabManager.Instance.DespawnPrefab(this.gameObject);
        }

        public void OnDespawned()
        {
        }

        protected override void Initialize()
        {
            base.Initialize();
            Direction = new Vector3(Random.Range(-.75f, .75f), Random.Range(0f, 1f), 0);
            Speed = Random.Range(3.0f, 6.0f);
            Distance = Random.Range(3.0f, 6.0f);
            mesh = this.gameObject.GetComponent<TextMesh>();
            mesh.renderer.sortingLayerName = SortingLayerConstants.SortingLayerNames.HighestLayer;
        }
        protected override void Deinitialize()
        {
        }

    }
}