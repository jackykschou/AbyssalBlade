using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Scripts.Managers;
using Assets.Scripts.Constants;
using Assets.Scripts.Attributes;
using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.DamageTextDespawn
{
    public class DamageTextDespawn : GameLogic
    {
        public float origDespawnTime = 1.0f;
        public float scrollingVelocity = 0.5f;
        private float timeAlive;
        private TextMesh mesh;

        protected override void Update()
        {
            mesh.transform.Translate(new Vector3(0, scrollingVelocity * Time.deltaTime, 0));
            timeAlive += Time.deltaTime;
        }

        public void OnSpawned()
        {
            mesh = this.gameObject.GetComponent<TextMesh>();
            mesh.renderer.sortingLayerName = SortingLayerConstants.SortingLayerNames.HighestLayer;
            timeAlive = 0.0f;
            this.StartCoroutine(this.TimedDespawn());
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
        }
        protected override void Deinitialize()
        {
        }

    }
}