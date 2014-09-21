﻿using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Misc
{
    [AddComponentMenu("Misc/LifeTimeLimit")]
    public class LifeTimeLimit : GameLogic 
    {
        [Range(0f, float.MaxValue)]
        public float LifeTime;

        protected override void Initialize()
        {
            base.Initialize();
            gameObject.layer = LayerMask.NameToLayer(LayerConstants.LayerNames.Projectile);
            DisableGameObject(LifeTime);
        }

        protected override void Deinitialize()
        {
        }
    }
}
