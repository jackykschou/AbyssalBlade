using Assets.Scripts.GameScripts.Components.PrefabLoader;
using UnityEngine;

using GameLogicEvent = Assets.Scripts.Constants.GameLogicEvent;
using GameLogicEventAttribute = Assets.Scripts.Attributes.GameLogicEvent;
using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventtAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    public class SpawnPrefabEffect : SkillEffect
    {
        public enum SpawnPosition
        {
            Center,
            Front,
            Back,
            Left,
            Right
        }

        public SpawnPosition Position;

        public PrefabSpawner Spawner;

        [GameLogicEventAttribute(GameLogicEvent.SkillCastTriggerSucceed)]
        public void Spawn()
        {
            Vector2 pos;
            if (GameView == null)
            {
                Debug.LogWarning("GameView is null");
                pos = transform.position;
            }
            else
            {
                switch (Position)
                {
                    case SpawnPosition.Front:
                        pos = GameView.ForwardEdge;
                        break;
                    case SpawnPosition.Back:
                        pos = GameView.BackwardEdge;
                        break;
                    case SpawnPosition.Left:
                        pos = GameView.LeftwardEdge;
                        break;
                    case SpawnPosition.Right:
                        pos = GameView.RightwardEdge;
                        break;
                    default:
                        pos = GameView.CenterPosition;
                        break;
                }
            }
            Spawner.SpawnPrefab(new Vector3(pos.x, pos.y, transform.position.z));
        }

        protected override void Deinitialize()
        {
        }
    }
}
