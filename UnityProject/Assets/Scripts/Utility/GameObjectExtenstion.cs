using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts;
using Assets.Scripts.GameScripts.GameLogic.Destroyable;
using UnityEngine;

namespace Assets.Scripts.Utility
{
    public static class GameObjectExtenstion 
    {
        public static void TriggerGameScriptEvent(this GameObject o, GameScriptEvent gameScriptEvent, params object[] args)
        {
            GameScript s = o.GetComponent<GameScript>();
            if (s == null)
            {
                Debug.LogWarning("GameObject " + o.name + "does not contains any GameScript to trigger the event " + gameScriptEvent);
                return;
            }

            s.TriggerGameScriptEvent(gameScriptEvent, args);
        }

        public static bool IsDestroyed(this GameObject o)
        {
            Destroyable destroyable = o.GetComponent<Destroyable>();
            if (destroyable == null)
            {
                return false;
            }

            return destroyable.Destroyed;
        }
    }
}
