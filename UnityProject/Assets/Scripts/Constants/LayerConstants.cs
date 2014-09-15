using UnityEngine;

namespace Assets.Scripts.Constants
{
    public class LayerConstants
    {
        private static class LayerNames
        {
            public const string StaticObstacle = "StaticObstacle";
            public const string Projectile = "Projectile";
            public const string DamageArea = "DamageArea";
            public const string Enemy = "Enemy";
            public const string PlayerCharacter= "PlayerCharacter";
            public const string Destroyable = "Destroyable";
            public const string DestroyableObstacle = "DestroyableObstacle";
        }

        public static class Layer
        {
            public static int StaticObstacle
            {
                get { return LayerMask.NameToLayer(LayerNames.StaticObstacle); }
            }
            public static int Projectile
            {
                get { return LayerMask.NameToLayer(LayerNames.Projectile); }
            }
            public static int DamageArea
            {
                get { return LayerMask.NameToLayer(LayerNames.DamageArea); }
            }
            public static int Enemy
            {
                get { return LayerMask.NameToLayer(LayerNames.Enemy); }
            }
            public static int PlayerCharacter
            {
                get { return LayerMask.NameToLayer(LayerNames.PlayerCharacter); }
            }
            public static int Destroyable
            {
                get { return LayerMask.GetMask(LayerNames.Destroyable, LayerNames.PlayerCharacter, LayerNames.Enemy); }
            }
            public static int DestroyableObstacle
            {
                get { return LayerMask.NameToLayer(LayerNames.DestroyableObstacle); }
            }
            public static int Obstacle
            {
                get { return LayerMask.GetMask(LayerNames.StaticObstacle, LayerNames.DestroyableObstacle); }
            }
            public static int Character
            {
                get { return LayerMask.GetMask(LayerNames.PlayerCharacter, LayerNames.Enemy); }
            }
        }
    }
}
