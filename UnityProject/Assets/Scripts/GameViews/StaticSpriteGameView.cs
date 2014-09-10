using UnityEngine;
using Assets.Scripts.Constants;

namespace Assets.Scripts.GameViews
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class StaticSpriteGameView : GameView
    {
        protected SpriteRenderer _render;

        protected override void Initialize()
        {
            _render = GetComponent<SpriteRenderer>();

            UpdateSortingOrder();
        }

        protected override void Deinitialize()
        {
        }

        protected void UpdateSortingOrder()
        {
            _render.sortingOrder = (int)(transform.position.y * WorldScaleConstant.LayerSortingScale);
        }
    }
}
