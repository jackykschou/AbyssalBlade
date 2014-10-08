﻿namespace Assets.Scripts.Constants
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
        PlayerDashButtonPressed,                // void()

        UpdateFacingDirection,                  // void(FacingDirection)

        SetAnimatorBoolState,                   // void(string)
        SetAnimatorIntState,                    // void(string, int)
        
        AIRotateToTarget,                       // void()
        AICastSkill,                            // void()
        AIMove,                                 // void(Vector2)

        CharacterMove,                          // void(Vector2)
        OnCharacterMove,                        // void(Vector2)
        PushCharacter,                          // void(vector2, float)

        SkillCastTriggerSucceed,                // void(Skill)
        SkillCastTriggerFailed,                 // void(Skill)
        UpdateSkillCooldownPercentage,          // void(Skill, float)
        OnNewTargetDiscovered,                  // void(GameObject)

        OnObjectMove,                           // void()
        OnObjectHasNoHitPoint,                  // void()
        OnObjectDestroyed,                      // void()
        InterruptCharacter,                     // void()
        OnCharacterInterrupted,                 // void()
        OnCharacterKnockBacked,                 // void(Vector2, float)
        ObjectChangeHealthFix,                    // void(float)
        ObjectChangeCurrentPercentageHealth,      // void(float)
        ObjectChangeMaxPercentageHealth,          // void(float)
        ObjectChangeFixHealthPerSec,              // void(float, int)
        ObjectChangeCurrentPercentageHealthPerSec,// void(float, int)
        ObjectChangeMaxPercentageHealthPerSec,    // void(float, int)
        OnObjectTakeDamage,                       // void(float)
        OnObjectTakeHeal,                          // void(float)

        OnCollideTriggerTriggered,          // void(GameObject)

        UpdateSectionId,                    // void(int)

        MenuStartButtonPressed,                          // void()
        MenuOptionsButtonPressed,                          // void()
        MenuQuitButtonPressed,                          // void()
        OnButtonMouseOver,                           // void(GameObject, int)
        OnButtonMouseLeave,                          // void(int)
        CameraFollowTarget                           // void(Transform)
    };
}
