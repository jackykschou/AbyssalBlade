﻿namespace Assets.Scripts.Constants
{
    public static class TagConstants
    {
        public const string PlayerTag = "Player";
        public const string EnemyTag = "Enemy";
        public const string NeutralTag = "Neutral";

        public static bool IsEnemy(string tag1, string tag2)
        {
            if (tag1 == NeutralTag)
            {
                return true;
            }

            return tag1 != tag2;
        }
    }
}
