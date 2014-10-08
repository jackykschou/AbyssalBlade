namespace Assets.Scripts.Constants
{
    //Add more events here, please specify the signiture of the event
    public enum GameEvent
    {
#if DEBUG
        ExampleEvent,                 // void()
#endif
        OnSectionActivated,                     // void(int)
        OnSectionDeactivated,                   // void(int)
        OnSectionObjectivesCompleted,           // void(int)
        OnSectionEnemySpawnPointActivated,      // void(GameObject, int)
        OnSectionEnemySpawnPointDeactivated,    // void(GameObject, int)
        OnSectionEnemySpawned,                  // void(GameObject, int)
        OnSectionEnemyDespawned,                // void(GameObject, int)

        OnPlayerSkillCoolDownUpdate,   // void(int, float)
        PlayerHealthUpdate,            // void(float)
        OnLevelStartLoading,           // void()
        OnLevelFinishedLoading,        // void()
        OnLevelEnded,                  // void()

        OnGameEventSent                // void(GameEvent)
     };
}
