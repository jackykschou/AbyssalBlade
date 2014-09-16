using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Projectile
{
    public abstract class Projectile : GameLogic
    {
        [Range(0f, float.MaxValue)]
        public float LifeTime;

        protected override void Initialize()
        {
            base.Initialize();
            gameObject.layer = LayerMask.NameToLayer(LayerConstants.LayerNames.Projectile);
            if (LifeTime > 0)
            {
                Invoke("DisableGameObject", LifeTime);
            }
        }

        protected override void Deinitialize()
        {
        }

        protected override void Update () {
        }
    }
}
