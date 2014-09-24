namespace Assets.Scripts.Constants
{
    //Add more events here, please specify the signiture of the event
    public enum GameScriptEvent
    {
#if DEBUG
        Example, // void()
#endif
        PlayerAxisMoved,                        // void(Vector2)
        UpdatePlayerAxis,                       // void(Vector2)
        PlayerAttack1ButtonPressed,             // void()
        PlayerAttack2ButtonPressed,             // void()
        PlayerAttack3ButtonPressed,             // void()
        PlayerAttack4ButtonPressed,             // void()

        UpdateFacingDirection,                  // void(FacingDirection)

        SetAnimatorState,                       // void(string)
        
        AIRotateToTarget,                       // void()
        AICastSkill,                            // void()
        AIMove,                                 // void(Vector2)

        MoveCharacter,                          // void(Vector2)
        OnCharacterMove,                        // void(Vector2)

        SkillCastTriggerSucceed,                // void(Skill)
        SkillCastTriggerFailed,                 // void(Skill)
        UpdateSkillCooldownPercentage,          // void(Skill, float)
        OnNewTargetDiscovered,                  // void(GameObject)

        OnObjectMove,                           // void()
        OnObjectDestroyed,                      // void()
        ObjectTakeFixDamage,                    // void(float)
        ObjectTakeCurrentPercentageDamage,      // void(float)
        ObjectTakeMaxPercentageDamage,          // void(float)
        ObjectTakeFixDamagePerSec,              // void(float, int)
        ObjectTakeCurrentPercentageDamagePerSec,// void(float, int)
        ObjectTakeMaxPercentageDamagePerSec,    // void(float, int)
        OnObjectTakeDamage                      // void(float)
    };
}
