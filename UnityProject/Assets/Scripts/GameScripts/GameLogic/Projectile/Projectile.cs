using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Projectile
{
    public class Projectile : GameLogic 
    {


        protected override void Initialize()
        {
            base.Initialize();
            gameObject.layer = LayerMask.NameToLayer(LayerConstants.LayerNames.Projectile);
        }

        protected override void Deinitialize()
        {
        }

        protected override void Update () {
        }
    }
}
