﻿namespace Assets.Scripts.Constants
{
    //Add more events here, please specify the signiture of the event
    public enum GameScriptEvent
    {
#if DEBUG
        Example, // void()
#endif
        UpdateProjectileDirection,              // void(Vector2)
        UpdateProjectileTarget,                 // void(Transform)
        ShootProjectile,                        // void()
        OnProjectileArriveDestination,          // void(Vector2)

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

        OnGameValueCurrentValueChanged,         // void(GameValue, GameValueChanger, float, bool)
        OnGameValueChangerApplyChange,          // void(GameValueChanger, GameValue, float, bool)
        OnObjectMove,                           // void()
        OnObjectHasNoHitPoint,                  // void()
        OnObjectDestroyed,                      // void()
        InterruptCharacter,                     // void()
        OnCharacterInterrupted,                 // void()
        OnCharacterKnockBacked,                 // void(Vector2, float)
        ObjectChangeHealth,                     // void(GameValueChanger)
        OnObjectTakeDamage,                     // void(float, bool, GameValue)
        OnObjectTakeHeal,                       // void(float, bool, GameValue)
        OnObjectHealthChanged,                  // void(float, GameValue)
        OnPhysicsBodyOnTriggerEnter2D,          // void(Collider2D)
        OnPhysicsBodyOnTriggerStay2D,           // void(Collider2D)
        OnPhysicsBodyOnTriggerExit2D,           // void(Collider2D)
        OnPhysicsBodyOnCollisionEnter2D,        // void(Collision2D)
        OnPhysicsBodyOnCollisionStay2D,         // void(Collision2D)
        OnPhysicsBodyOnCollisionExit2D,         // void(Collision2D)

        UpdateSectionId,                         // void(int)

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

        ButtonChange,                            // void(int buttonID)
        ResetHealth,                             // void()
        ChangeDamageCriticalChance,              // void(float)
        ChangeDamageReduction,                   // void(float)
        LoadingScreenStartLoading,               // void()
        SetHealthInvincibility,                  // void(bool)
        OnHealthInvincibleEnable,                // void()
        OnHealthInvincibleDisable,               // void()
        SpawnPrefabOnSpriteGameViewOnRandomPosition,              // void(Prefab)

        UpdateProjectileDestination,                 // void(Vector2)
    };
}
