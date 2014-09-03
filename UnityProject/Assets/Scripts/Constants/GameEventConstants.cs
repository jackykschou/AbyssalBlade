using System;
using System.Collections.Generic;
using Assets.Scripts.Exceptions;

namespace Assets.Scripts.Constants
{
    public static class GameEventConstants
    {
        public delegate void ExampleEvent();

        //Add more events here, please specify the signiture of the event
        public enum GameEvent
        {
#if DEBUG
            ExampleEvent    // void()
#endif
        };
    }
}
