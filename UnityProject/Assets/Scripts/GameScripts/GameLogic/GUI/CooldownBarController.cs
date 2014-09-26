using Assets.Scripts.Attributes;
using UnityEngine;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using UnityEngine.UI;
using Assets.Scripts.GameScripts.GameLogic.Skills;
using Assets.Scripts.GameScripts.Components.GameValue;
using System.Collections.Generic;


namespace Assets.Scripts.GameScripts.GameLogic
{
    public class CooldownBarController : GameLogic
    {
        [HideInInspector]
        public Skills.SkillCasters.PlayerCharacterSkillsCaster pCaster;
        public List<Slider> CooldownBars;

        protected override void Initialize()
        {
            base.Initialize();
            pCaster = this.gameObject.GetComponent<Skills.SkillCasters.PlayerCharacterSkillsCaster>();
            foreach (var slider in CooldownBars)
                slider.value = 1;
        }

        protected override void Update()
        {
            for (int i = 1; i < pCaster.Skills.Count && i < CooldownBars.Count+1; i++)
                CooldownBars[i-1].value = pCaster.Skills[i].getCooldownPercentage();
        }

        protected override void Deinitialize()
        {
        }
    }
}
