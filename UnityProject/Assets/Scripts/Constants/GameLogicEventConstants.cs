namespace Assets.Scripts.Constants
{
    //Add more events here, please specify the signiture of the event
    public enum GameLogicEvent
    {
#if DEBUG
        Example, // void()
#endif
        AxisMoved // void(Vector2)
    };
}
