using UnityEngine;

namespace Assets.Scripts.GameViews
{
    [AddComponentMenu("GameView/MovableAnimatedSpriteGameView")]
    public class MovableAnimatedSpriteGameView : StaticAnimatedSpriteGameView
    {
        protected override void Update()
        {
            UpdateSortingOrder();
        }
    }
}
