﻿using UnityEngine;
using Assets.Scripts.Constants;

namespace Assets.Scripts.GameViews
{
    [AddComponentMenu("GameView/StaticSpriteGameView")]
    [RequireComponent(typeof(SpriteRenderer))]
    public class StaticSpriteGameView : GameView
    {
        protected SpriteRenderer _render;

        protected override void Initialize()
        {
            base.Initialize();

            _render = GetComponent<SpriteRenderer>();
            _render.sortingLayerName = SortingLayerConstants.SortingLayerNames.CharacterLayer;
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
    }
}
