using System.Collections;
using Assets.Scripts.Constants;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Misc
{
    public class ChangeLevelOnObjectDestroy : GameLogic
    {
        public Prefab LevelPrefab;
        [Range(0, 100f)]
        public float Delay;

        private bool _levelChanged;

        protected override void Initialize()
        {
            base.Initialize();
            _levelChanged = false;
        }

        [Attributes.GameScriptEvent(GameScriptEvent.OnObjectDestroyed)]
        public void OnObjectHasNoHitPoint()
        {
            if (!_levelChanged)
            {
                _levelChanged = true;
                GameManager.Instance.ChangeLevel(LevelPrefab);
            }
        }

        protected override void Deinitialize()
        {
        }
    }
}
