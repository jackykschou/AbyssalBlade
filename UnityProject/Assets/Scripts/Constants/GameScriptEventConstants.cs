namespace Assets.Scripts.Constants
{
    //Add more events here, please specify the signiture of the event
    public enum GameScriptEvent
    {
#if DEBUG
        Example, // void()
#endif
        UpdateFacingDirection,                  // void(FacingDirection)
        UpdateMoveDirection,                    // void(Vector2)

        SetAnimatorBoolState,                   // void(string)
        SetAnimatorFloatState,                  // void(string, int)
        
        CharacterRigidMove,                     // void(Vector2)
        CharacterNonRigidMove,                  // void(Vector2)
        OnCharacterMove,                        // void(Vector2)
        PushCharacter,                          // void(vector2, float)

        RotateTowardsTarget,                     // void()

        SkillCastTriggerSucceed,                // void(Skill)
        SkillCastTriggerFailed,                 // void(Skill)
        SkillCastFinished,                      // void(Skill)
        UpdateSkillCooldownPercentage,          // void(Skill, float)
        OnNewTargetDiscovered,                  // void(GameObject)
        UpdateSkillButtonHoldEffectTime,        // void(float)
        OnSkillComboChanged,                    // void(float)
        RefreshSkillCoolDown,                   // void()

        OnGameValueCurrentValueChanged,         // void(GameValue, float, bool)
        OnObjectMove,                           // void()
        OnObjectHasNoHitPoint,                  // void()
        OnObjectDestroyed,                      // void()
        InterruptCharacter,                     // void()
        OnCharacterInterrupted,                 // void()
        OnCharacterKnockBacked,                 // void(Vector2, float)
        ObjectChangeHealth,                     // void(GameValueChanger)
        OnObjectTakeDamage,                     // void(float, bool)
        OnObjectTakeHeal,                       // void(float)
        OnPhysicsBodyOnTriggerEnter2D,          // void(Collider2D)
        OnPhysicsBodyOnTriggerStay2D,           // void(Collider2D)
        OnPhysicsBodyOnTriggerExit2D,           // void(Collider2D)
        OnPhysicsBodyOnCollisionEnter2D,        // void(Collision2D)
        OnPhysicsBodyOnCollisionStay2D,        // void(Collision2D)
        OnPhysicsBodyOnCollisionExit2D,        // void(Collision2D)

        UpdateSectionId,                    // void(int)

        MenuStartButtonPressed,                  // void()
        MenuOptionsButtonPressed,                // void()
        MenuQuitButtonPressed,                   // void()
        OnButtonMouseOver,                       // void(GameObject, int)
        OnButtonMouseLeave,                      // void(int)
        CameraFollowTarget,                      // void(Transform)
        GateDeactivated,                         // void()
        GateActivated,                           // void()

        ChangeObjectMotorSpeed,                  // void(GameValueChanger)
        UnchangeObjectMotorSpeed,                // void(GameValueChanger)

        ButtonChange                             // void(int buttonID)
    };
}
