using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers
{
    [AddComponentMenu("LevelMechanics/TeleportPlayerOnEnter")]
    public class TeleportPlayerOnPickUp : TargetEffectApplier
    {
        protected override void ApplyEffect(GameObject target)
        {
           // Vector3 nextSpawnPos = SurvivalModeManager.Instance.GetNextSpawn();
           // GameManager.Instance.PlayerMainCharacter.transform.position = new Vector3(nextSpawnPos.x, nextSpawnPos.y, GameManager.Instance.PlayerMainCharacter.transform.position.z);
        }
    }
}
