namespace Assets.Scripts.Constants
{
    //Add more events here, please specify the signiture of the event
    public enum GameEvent
    {
#if DEBUG
        ExampleEvent,                 // void()
#endif
        OnPlayerSkillCoolDownUpdate,   // void(int, float)
        PlayerHealthUpdate             // void(float)
     };
}
