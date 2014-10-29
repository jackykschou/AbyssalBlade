using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameViews
{
    [AddComponentMenu("GameView/StaticSpriteGameView")]
    [RequireComponent(typeof(SpriteRenderer))]
    public class StaticSpriteGameView : GameView
    {
        protected SpriteRenderer _render;

        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            _render = GetComponent<SpriteRenderer>();
            if (_render.sortingLayerName == SortingLayerConstants.SortingLayerNames.Default)
            {
                _render.sortingLayerName = SortingLayerConstants.SortingLayerNames.CharacterLayer;
            }
        }

        protected override void Initialize()
        {
            base.Initialize();
            renderer.enabled = true;
            UpdateSortingOrder();
        }

        protected override void Deinitialize()
        {
        }

        public override Vector2 CenterPosition
        {
            get { return _render.bounds.center; }
        }

        public override Vector2 ForwardEdge
        {
            get
            {
                switch (FacingDirection)
                {
                    case FacingDirection.Up:
                        return _render.bounds.center + new Vector3(0, Mathf.Abs(_render.bounds.extents.y), 0);
                    case FacingDirection.Down:
                        return _render.bounds.center - new Vector3(0, Mathf.Abs(_render.bounds.extents.y), 0);
                    case FacingDirection.Left:
                        return _render.bounds.center - new Vector3(Mathf.Abs(_render.bounds.extents.x), 0, 0);
                    default:
                        return _render.bounds.center + new Vector3(Mathf.Abs(_render.bounds.extents.x), 0, 0);
                }
            }
        }

        public override Vector2 BackwardEdge
        {
            get
            {
                switch (FacingDirection)
                {
                    case FacingDirection.Up:
                        return _render.bounds.center - new Vector3(0, Mathf.Abs(_render.bounds.extents.y), 0);
                    case FacingDirection.Down:
                        return _render.bounds.center + new Vector3(0, Mathf.Abs(_render.bounds.extents.y), 0);
                    case FacingDirection.Left:
                        return _render.bounds.center + new Vector3(Mathf.Abs(_render.bounds.extents.x), 0, 0);
                    default:
                        return _render.bounds.center - new Vector3(Mathf.Abs(_render.bounds.extents.x), 0, 0);
                }
            }
        }

        public override Vector2 LeftwardEdge
        {
            get
            {
                switch (FacingDirection)
                {
                    case FacingDirection.Up:
                        return _render.bounds.center - new Vector3(Mathf.Abs(_render.bounds.extents.x), 0, 0);
                    case FacingDirection.Down:
                        return _render.bounds.center + new Vector3(Mathf.Abs(_render.bounds.extents.y), 0, 0);
                    case FacingDirection.Left:
                        return _render.bounds.center - new Vector3(0, Mathf.Abs(_render.bounds.extents.y), 0);
                    default:
                        return _render.bounds.center + new Vector3(0, Mathf.Abs(_render.bounds.extents.y), 0);
                }
            }
        }

        public override Vector2 RightwardEdge
        {
            get
            {
                switch (FacingDirection)
                {
                    case FacingDirection.Up:
                        return _render.bounds.center + new Vector3(Mathf.Abs(_render.bounds.extents.x), 0, 0);
                    case FacingDirection.Down:
                        return _render.bounds.center - new Vector3(Mathf.Abs(_render.bounds.extents.y), 0, 0);
                    case FacingDirection.Left:
                        return _render.bounds.center + new Vector3(0, Mathf.Abs(_render.bounds.extents.y), 0);
                    default:
                        return _render.bounds.center - new Vector3(0, Mathf.Abs(_render.bounds.extents.y), 0);
                }
            }
        }

        protected void UpdateSortingOrder()
        {
            _render.sortingOrder = (int)(transform.position.y * WorldScaleConstant.LayerSortingScale);
        }

        [Attributes.GameScriptEvent(GameScriptEvent.OnObjectDestroyed)]
        public void DisableRender()
        {
            renderer.enabled = false;
        }
    }
}
