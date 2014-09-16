namespace Assets.Scripts.Constants
{
    //Add more events here, please specify the signiture of the event
    public enum GameScriptEvent
    {
#if DEBUG
        Example, // void()
#endif
        PlayerAxisMoved, // void(Vector2)
        PlayerAttack1ButtonPressed,     // void()
        PlayerAttack2ButtonPressed,     // void()
        PlayerAttack3ButtonPressed,     // void()
        PlayerAttack4ButtonPressed,     // void()

        UpdateFacingDirection,          // void(FacingDirection)

        SkillCastTriggerSucceed,        // void(Skill)
        SkillCastTriggerFailed,         // void(Skill)
        SkillEnded,                     // void(Skill)
        UpdateSkillCooldownPercentage,  // void(Skill, float)

        OnObjectMove,                   // void()
        OnDestroyableDestroyed,         // void()
        OnObjectDestroyed,              // void()
        OnObjectTakeDamage              // void(float)
    };
}
