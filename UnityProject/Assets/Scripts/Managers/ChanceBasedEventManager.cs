using System.Collections.Generic;
using Assets.Scripts.GameScripts.GameLogic;
using Assets.Scripts.Utility;

namespace Assets.Scripts.Managers
{
    public enum ChanceBasedEvent
    {
        Event1,
        Event2
    }

    public class ChanceBasedEventManager : GameLogic
    {
        public List<string> ChanceBasedEvents; 
        public List<float> EventBaseChances; 
        public List<float> EventCurrentChances;

        private static ChanceBasedEventManager _instance;
        public static ChanceBasedEventManager Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = FindObjectOfType<ChanceBasedEventManager>();
                    DontDestroyOnLoad(_instance.gameObject);
                }
                return _instance;
            }
        }

        public bool RollEventChance(ChanceBasedEvent Event)
        {
            return UtilityFunctions.RollChance(EventCurrentChances[(int)Event]);
        }

        public void ChangeEventCurrentChanceBy(ChanceBasedEvent Event, float amount)
        {
            EventCurrentChances[(int) Event] += amount;
        }

        public void ChangeEventCurrentChanceTo(ChanceBasedEvent Event, float chance)
        {
            EventCurrentChances[(int)Event] = chance;
        }

        public void ChangeEventBaseChanceBy(ChanceBasedEvent Event, float amount)
        {
            EventBaseChances[(int)Event] += amount;
        }

        public void ChangeEventBaseChanceTo(ChanceBasedEvent Event, float chance)
        {
            EventBaseChances[(int)Event] = chance;
        }

        public void ResetEventToBaseChance(ChanceBasedEvent Event)
        {
            EventCurrentChances[(int) Event] = EventBaseChances[(int) Event];
        }

        public void ResetAllEventsToBaseChance()
        {
            System.Array enumValues = System.Enum.GetValues(typeof(ChanceBasedEvent));
            for (int i = 0; i < enumValues.Length; ++i)
            {
                ResetEventToBaseChance((ChanceBasedEvent) enumValues.GetValue(i));
            }
        }

        protected override void Deinitialize()
        {
        }

        public override void EditorUpdate()
        {
            base.EditorUpdate();
            System.Array enumValues = System.Enum.GetValues(typeof(ChanceBasedEvent));
            ChanceBasedEvents.Resize(enumValues.Length);
            for(int i = 0; i < enumValues.Length; ++i)
            {
                ChanceBasedEvents[i] = ((ChanceBasedEvent) enumValues.GetValue(i)).ToString();
            }
            EventBaseChances.Resize(enumValues.Length);
            EventCurrentChances.Resize(enumValues.Length);
        }
    }
}
