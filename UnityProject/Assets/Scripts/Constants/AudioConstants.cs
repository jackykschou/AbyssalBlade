using System.Collections.Generic;
using System;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Constants
{
    public enum CueName
    {
        Random1,
        Parallel1
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
        Shot2,
		Stone_Enemy_Death_Rattle,
		Stone_Enemy_Death_Rattle_2,
		Stone_Enemy_Death_Rattle_3,
		Strike,
		Swipe,
		Warp_In,
		Warp_In_2,
		Warp_In_3,
		Warp_In_4,
		Warp_In_5
    };

    public enum LoopName
    {
        MainLoop
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
            {ClipName.Shot2, "Shot2"},
			{ClipName.Stone_Enemy_Death_Rattle, "Stone_Enemy_Death_Rattle"},
			{ClipName.Stone_Enemy_Death_Rattle_2, "Stone_Enemy_Death_Rattle_2"},
			{ClipName.Stone_Enemy_Death_Rattle_3, "Stone_Enemy_Death_Rattle_3"},
			{ClipName.Strike, "Strike"},
            {ClipName.Swipe, "Swipe"},
			{ClipName.Warp_In, "Warp_In"},
			{ClipName.Warp_In_2, "Warp_In_2"},
			{ClipName.Warp_In_3, "Warp_In_3"},
			{ClipName.Warp_In_4, "Warp_In_4"},
			{ClipName.Warp_In_5, "Warp_In_5"}
        };

        private static readonly Dictionary<CueName, string> AudioCueNames = new Dictionary<CueName, string>()
        {
            {CueName.Random1, "Random1"},
            {CueName.Parallel1, "Parallel1"}
        };


        private static readonly Dictionary<LoopName, string> AudioLoopNames = new Dictionary<LoopName, string>()
        {
            {LoopName.MainLoop, "MainLoop"}
        };

        public static void CreateCustomCues()
        {
            // Examples
            List<ClipName> loopList = new List<ClipName> { ClipName.HackandSlash };
            AudioManager.Instance.createLoop(LoopName.MainLoop, loopList);

            List<ClipName> randList = new List<ClipName>();
            randList.Add(ClipName.Shot);
            randList.Add(ClipName.Strike);
            randList.Add(ClipName.Swipe);
            AudioManager.Instance.createMultiCueRandom(CueName.Random1,randList);


            List<ClipName> parallelList = new List<ClipName>();
            randList.Add(ClipName.Swipe);
            randList.Add(ClipName.Strike);
            AudioManager.Instance.createMultiCueParallel(CueName.Parallel1, parallelList);
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
                throw new Exception("Cue is not defined");
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
