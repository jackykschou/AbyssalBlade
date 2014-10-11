﻿using System.Collections.Generic;
using System;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Constants
{
    public enum CueName
    {
        Random1,
        Parallel1,
        Forest_Loop_Light,
        Forest_Loop_Medium,
        Forest_Loop_Heavy
    };

    public enum ClipName
    {
        Char_Take_Damage,
		Dash,
		Death_Rattle_Char_1,
		Death_Sound_1,
		Death_Sound_2,
		Death_Sound_3,
        Footstep,
		Forest_Level_2_Full,
		Forest_Level_2_Main_Loop,
        HackandSlash,
        Laser,
        Laser2,
        Laser3,
        MenuSound,
        MetalClang,
        MetalClang2,
        MissEnemy,
        Random,
        Shot,
		Strike,
		Swipe,
		Warp_In,
		Warp_In_2,
		Warp_In_3,
		Warp_In_4,
		Warp_In_5,
        Forest_Level_Basic,
        Forest_Level_Hard_Full,
        Forest_Level_Hard_Loop2,
        Forest_Level_Medium,
        Forest_Level_Medium_2MLoop,
        Forest_Level_Resolution,
		Heavy_Attack,
		Heavy_Attack_2,
		Heavy_Attack_3,
		Shot_2,
		Stone_Enemy_Death_Rattle,
		Stone_Enemy_Death_Rattle_2,
		Stone_Enemy_Take_Damage,
		Stone_Enemy_Take_Damage_2
    };

    public enum LoopName
    {
        MainLoop,
        Forest_Level_Loop,
        TestMultiLoop
    };


    public class AudioConstants
    {
        public const string AudioExtension = ".mp3";
        public const string StartingAssetAudioPath = "Assets/Resources/Arts/Music/Cues/";

        private static readonly Dictionary<ClipName, string> AudioClipNames = new Dictionary<ClipName, string>()
        {
			{ClipName.Char_Take_Damage, "Char_Take_Damage"},
			{ClipName.Dash, "Dash"},
			{ClipName.Death_Rattle_Char_1, "Death_Rattle_Char_1"},
			{ClipName.Death_Sound_1, "Death_Sound_1"},
			{ClipName.Death_Sound_2, "Death_Sound_2"},
			{ClipName.Death_Sound_3, "Death_Sound_3"},
            {ClipName.Footstep, "Footstep"},
			{ClipName.Forest_Level_2_Full, "Forest_Level_2_Full"},
			{ClipName.Forest_Level_2_Main_Loop, "Forest_Level_2_Main_Loop"},
            {ClipName.HackandSlash, "HackandSlash"},
            {ClipName.Laser, "Laser"},
            {ClipName.Laser2, "Laser2"},
            {ClipName.Laser3, "Laser3"},
            {ClipName.MenuSound, "MenuSound"},
            {ClipName.MetalClang, "MetalClang"},
            {ClipName.MetalClang2, "MetalClang2"},
            {ClipName.MissEnemy, "MissEnemy"},
            {ClipName.Random, "Random"},
            {ClipName.Shot, "Shot"},
			{ClipName.Strike, "Strike"},
            {ClipName.Swipe, "Swipe"},
			{ClipName.Warp_In, "Warp_In"},
			{ClipName.Warp_In_2, "Warp_In_2"},
			{ClipName.Warp_In_3, "Warp_In_3"},
			{ClipName.Warp_In_4, "Warp_In_4"},
			{ClipName.Warp_In_5, "Warp_In_5"},
			{ClipName.Forest_Level_Basic, "Forest_Level_Basic"},
			{ClipName.Forest_Level_Hard_Full, "Forest_Level_Hard_Full"},
			{ClipName.Forest_Level_Hard_Loop2, "Forest_Level_Hard_Loop2"},
			{ClipName.Forest_Level_Medium, "Forest_Level_Medium"},
			{ClipName.Forest_Level_Medium_2MLoop, "Forest_Level_Medium_2MLoop"},
			{ClipName.Forest_Level_Resolution, "Forest_Level_Resolution"},
			{ClipName.Heavy_Attack, "Heavy_Attack"},
			{ClipName.Heavy_Attack_2, "Heavy_Attack_2"},
			{ClipName.Heavy_Attack_3, "Heavy_Attack_3"},
			{ClipName.Shot_2, "Shot_2"},
			{ClipName.Stone_Enemy_Death_Rattle, "Stone_Enemy_Death_Rattle"},
			{ClipName.Stone_Enemy_Death_Rattle_2, "Stone_Enemy_Death_Rattle_2"},
			{ClipName.Stone_Enemy_Take_Damage, "Stone_Enemy_Take_Damage"},
			{ClipName.Stone_Enemy_Take_Damage_2, "Stone_Enemy_Take_Damage_2"}
        };

        private static readonly Dictionary<CueName, string> AudioCueNames = new Dictionary<CueName, string>()
        {
            {CueName.Random1, "Random1"},
            {CueName.Parallel1, "Parallel1"},
            {CueName.Forest_Loop_Light, "Forest_Loop_Light"},
            {CueName.Forest_Loop_Medium, "Forest_Loop_Medium"},
            {CueName.Forest_Loop_Heavy, "Forest_Loop_Heavy"}
        };


        private static readonly Dictionary<LoopName, string> AudioLoopNames = new Dictionary<LoopName, string>()
        {
            {LoopName.MainLoop, "MainLoop"},
            {LoopName.Forest_Level_Loop, "Forest_Level_Loop"},
            {LoopName.TestMultiLoop, "TestMultiLoop"}
        };

        public static void CreateCustomCues()
        {
            // Examples
            List<ClipName> loopList = new List<ClipName> 
            { 
                ClipName.HackandSlash 
            };
            AudioManager.Instance.createLoop(LoopName.MainLoop, loopList);
            /*
            List<ClipName> forestList = new List<ClipName> 
            { 
                ClipName.Forest_Level_2_Full,
                ClipName.Forest_Level_2_Main_Loop
            };
            AudioManager.Instance.createLoop(LoopName.Forest_Level_Loop, forestList, 0.5f);
            */


            /*
            List<ClipName> lightLoop = new List<ClipName>
            {
                ClipName.Forest_Level_Basic
            };
            List<ClipName> mediumLoop = new List<ClipName>
            {
                ClipName.Forest_Level_Basic,
                ClipName.Forest_Level_Medium
            };
            List<ClipName> heavyLoop = new List<ClipName>
            {
                ClipName.Forest_Level_Basic,
                ClipName.Forest_Level_Hard_Full
            };
            AudioManager.Instance.createMultiCueParallel(CueName.Forest_Loop_Light, lightLoop);
            AudioManager.Instance.createMultiCueParallel(CueName.Forest_Loop_Medium, mediumLoop);
            AudioManager.Instance.createMultiCueParallel(CueName.Forest_Loop_Heavy, heavyLoop);
            AudioManager.Instance.createMultiLoop(LoopName.TestMultiLoop, new List<CueName>
            {
                CueName.Forest_Loop_Light,
                CueName.Forest_Loop_Medium,
                CueName.Forest_Loop_Heavy
            });*/
            List<ClipName> levelLoopIntensities = new List<ClipName>
            {
                ClipName.Forest_Level_Basic,
                ClipName.Forest_Level_Medium,
                ClipName.Forest_Level_Hard_Full
            };
            AudioManager.Instance.createLevelLoop(LoopName.TestMultiLoop, levelLoopIntensities);

        }

        public static string GetClipName(ClipName name)
        {
            if (!AudioClipNames.ContainsKey(name))
                throw new Exception("Clip is not defined");
            return AudioClipNames[name];
        }

        public static string GetCueName(CueName name)
        {
            if (!AudioCueNames.ContainsKey(name))
                throw new Exception("Cue is not defined " + name);
            return AudioCueNames[name];
        }

        public static string GetLoopName(LoopName name)
        {
            if (!AudioLoopNames.ContainsKey(name))
                throw new Exception("Loop is not defined");
            return AudioLoopNames[name];
        }
    }
}
