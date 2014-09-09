namespace Assets.Scripts.GameViews
{
    public class MovableanimatedStaticSpriteGameView : StaticAnimatedStaticSpriteGameView
    {
        protected override void Update()
        {
            UpdateSortingOrder();
        }
    }
}
