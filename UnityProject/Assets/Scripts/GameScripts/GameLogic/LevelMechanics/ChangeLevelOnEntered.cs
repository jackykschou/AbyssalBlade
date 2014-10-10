using System.Collections;
using Assets.Scripts.Constants;
using Assets.Scripts.Managers;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.LevelMechanics
{
    [RequireComponent(typeof(Collider2D))]
    public class ChangeLevelOnEntered : GameLogic
    {
        public Collider2D AreaCollider;
        public Prefab ToLevel;
        [Range(0f, 10f)] 
        public float ChangeLevelDelay;
        private bool _changed;

        protected override void Initialize()
        {
            base.Initialize();
            AreaCollider = GetComponent<Collider2D>();
            AreaCollider.isTrigger = true;
            gameObject.layer = LayerConstants.LayerMask.LeaveLevelArea;
            AreaCollider.enabled = true;
            _changed = false;
        }

        protected override void Deinitialize()
        {
        }

        protected override void OnTriggerEnter2D(Collider2D coll)
        {
            base.OnTriggerEnter2D(coll);
            if (!GameManager.Instance.PlayerMainCharacter.HitPointAtZero() && !_changed)
            {
                _changed = true;
                AreaCollider.enabled = false;
                StartCoroutine(ChangeLevel());
            }
        }

        private IEnumerator ChangeLevel()
        {
            yield return new WaitForSeconds(ChangeLevelDelay);
            GameManager.Instance.ChangeLevel(ToLevel);
        }
    }
}
