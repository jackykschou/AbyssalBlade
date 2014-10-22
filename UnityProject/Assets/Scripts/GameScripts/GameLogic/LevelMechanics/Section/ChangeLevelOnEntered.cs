using System.Collections;
using Assets.Scripts.Constants;
using Assets.Scripts.Managers;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.LevelMechanics.Section
{
    [RequireComponent(typeof(Collider2D))]
    public class ChangeLevelOnEntered : SectionLogic
    {
        public Collider2D AreaCollider;
        public Prefab ToLevel;
        [Range(0f, 10f)] 
        public float ChangeLevelDelay;
        private bool _changed;
        private bool _activated;

        protected override void Initialize()
        {
            base.Initialize();
            AreaCollider = GetComponent<Collider2D>();
            AreaCollider.isTrigger = true;
            gameObject.layer = LayerConstants.LayerMask.LeaveLevelArea;
            AreaCollider.enabled = true;
            _changed = false;
            _activated = false;
        }

        protected override void Deinitialize()
        {
        }

        public override void OnSectionDeactivated(int sectionId)
        {
            base.OnSectionDeactivated(sectionId);
            if (sectionId == SectionId)
            {
                _activated = true;
            }
        }

        protected override void OnTriggerStay2D(Collider2D coll)
        {
            if (!GameManager.Instance.PlayerMainCharacter.HitPointAtZero() && !_changed && _activated)
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
