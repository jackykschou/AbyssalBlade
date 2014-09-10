namespace Assets.Scripts.GameViews
{
    public class MovableAnimatedSpriteGameView : StaticAnimatedSpriteGameView
    {
        protected override void Update()
        {
            UpdateSortingOrder();
        }
    }
}
