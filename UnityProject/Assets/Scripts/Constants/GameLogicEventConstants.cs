namespace Assets.Scripts.Constants
{
    //Add more events here, please specify the signiture of the event
    public enum GameLogicEvent
    {
#if DEBUG
        Example, // void()
#endif
        AxisMoved, // void(Vector2)

        PlayerAttack1ButtonPressed,     // void()
        PlayerAttack2ButtonPressed,     // void()
        PlayerAttack3ButtonPressed,     // void()
        PlayerAttack4ButtonPressed,     // void()

        UpdateFacingDirection,          // void(FacingDirection)

        SkillCastTriggerSucceed,        // void()
        SkillCastTriggerFailed,         // void()
        SkillEnded,                     // void()
        UpdateSkillCooldownPercentage,  // void(float)

        OnObjectMove,                   // void()

        OnObjectHaveNoHealth,           // void()
        GameObjectDestroyed,            // void()
        DamageTaked                     // void(float)
    };
}
