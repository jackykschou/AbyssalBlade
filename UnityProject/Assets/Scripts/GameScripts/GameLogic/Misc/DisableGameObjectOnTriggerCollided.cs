using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Misc
{
    [RequireComponent(typeof(Collider2D))]
    public class DisableGameObjectOnTriggerCollided : GameLogic
    {
        [Range(0f, 10f)]
        public float Delay = 0f;
        public List<int> TargetPhysicalLayers = new List<int>();

        private bool _gameObjectDisabled;
        private Collider2D _collider;

        protected override void OnTriggerEnter2D(Collider2D coll)
        {
            base.OnTriggerEnter2D(coll);
            if (TargetPhysicalLayers.Contains(coll.gameObject.layer))
            {
                DisableOnTriggerCollided();
            }
        }

        protected override void OnTriggerStay2D(Collider2D coll)
        {
            base.OnTriggerStay2D(coll);
            if (TargetPhysicalLayers.Contains(coll.gameObject.layer))
            {
                DisableOnTriggerCollided();
            }
        }

        private void DisableOnTriggerCollided()
        {
            if (!_gameObjectDisabled)
            {
                _collider.enabled = false;
                _gameObjectDisabled = true;
                DisableGameObject(Delay);
            }
        }

        protected override void Initialize()
        {
            base.Initialize();
            _gameObjectDisabled = false;
            _collider = GetComponent<Collider2D>();
            _collider.enabled = true;
            _collider.isTrigger = true;
        }

        protected override void Deinitialize()
        {
        }
    }
}
