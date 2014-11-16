using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameViews
{
    [AddComponentMenu("GameView/ParticleSystemView")]
    [RequireComponent(typeof (ParticleSystem))]
    public class ParticleSystemView : GameView
    {
        protected ParticleSystem _ParticleSystem;
        protected ParticleSystem[] _ParticleSystems;
        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            _ParticleSystem = GetComponent<ParticleSystem>();
            _ParticleSystem.renderer.sortingLayerName = SortingLayerConstants.SortingLayerNames.ForegroundLayer;
            _ParticleSystems = GetComponentsInChildren<ParticleSystem>();
        }

        protected override void Update()
        {
            base.Update();
            UpdateSortingOrder();
        }

        protected void UpdateSortingOrder()
        {
            _ParticleSystem.renderer.sortingOrder = (int)(transform.position.y * WorldScaleConstant.LayerSortingScale);
            foreach (ParticleSystem pSystem in _ParticleSystems)
            {
                pSystem.renderer.sortingOrder = (int)(transform.position.y * WorldScaleConstant.LayerSortingScale);
            }
        }

        protected override void Deinitialize()
        {
        }

        public override Vector2 CenterPosition
        {
            get { return transform.position; }
        }

        public override Vector2 ForwardEdge
        {
            get { return transform.position; }
        }

        public override Vector2 BackwardEdge
        {
            get { return transform.position; }
        }

        public override Vector2 LeftwardEdge
        {
            get { return transform.position; }
        }

        public override Vector2 RightwardEdge
        {
            get { return transform.position; }
        }
    }
}
