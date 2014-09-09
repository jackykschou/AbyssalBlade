namespace Assets.Scripts.GameViews
{
    public class MovableanimatedStaticSpriteGameView : StaticAnimatedStaticSpriteGameView
    {
        protected virtual void Update()
        {
            UpdateSortingOrder();
        }
    }
}
