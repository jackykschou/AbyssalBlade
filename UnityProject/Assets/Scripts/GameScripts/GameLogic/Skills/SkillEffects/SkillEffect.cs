using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    public class SkillEffect : GameLogic 
    {
        public GameObject ParentGameObject 
        {
            get { return transform.parent.gameObject; }
        }

        protected override void Deinitialize()
        {
        }
    }
}
