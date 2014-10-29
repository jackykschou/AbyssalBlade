﻿using System.Collections.Generic;
using System;
using System.IO;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Constants
{
    public enum Intensity
    {
        Light,
        Medium,
        Heavy
    };

    public enum CueName
    {
        Random1 = 0,
        Parallel1 = 1,
        Forest_Loop_Light = 2,
        Forest_Loop_Medium = 3,
        Forest_Loop_Heavy = 4,
        Warp_In_Random= 5
    };

    public enum ClipName
    {
        Char_Take_Damage    = 0,
		Dash                = 1,
		Death_Rattle_Char_1 = 2,
		Death_Sound_1       = 3,
		Death_Sound_2       = 4,
        Death_Sound_3       = 5,
        Footstep            = 6,
		Forest_Level_2_Full = 7,
		Forest_Level_2_Main_Loop = 8,
        HackandSlash        = 9,
        Laser               = 10,
        Laser2              = 11,
        Laser3              = 12,
        MenuSound           = 13,
        MetalClang          = 14,
        MetalClang2         = 15,
        MissEnemy           = 16,
        Random              = 17,
        Shot                = 18,
        Strike              = 19,
		Swipe               = 20,
		Warp_In             = 21,
        Warp_In_2           = 22,
        Warp_In_3           = 23,
        Warp_In_4           = 24,
		Warp_In_5           = 25,
        Forest_Level_Basic  = 26,
        Forest_Level_Hard_Full  = 27,
        Forest_Level_Hard_Loop2 = 28,
        Forest_Level_Medium     = 29,
        Forest_Level_Medium_2MLoop = 30,
        Forest_Level_Resolution = 31,
        Heavy_Attack        = 32,
        Heavy_Attack_2      = 33,
        Heavy_Attack_3      = 34,
        Shot_2              = 35,
        Stone_Enemy_Death_Rattle    = 36,
        Stone_Enemy_Death_Rattle_2  = 37,
        Stone_Enemy_Take_Damage     = 38,
        Stone_Enemy_Take_Damage_2   = 39,
        Dog_Attack          = 40,
        Dog_Attack_2        = 41,
        Dog_Attack_3        = 42,
        Dog_Attack_4        = 43,
        Dog_Death           = 44,
        Dog_Death_2         = 45,
        Dog_Death_3         = 46,
        Dog_Growl           = 47,
        Dog_Movement        = 48,
        Dog_Pant            = 49,
        Electric_Short      = 50,
        Electric_Short_2    = 51,
        Electric_Short_3    = 52,
        Forest_Ambience     = 53,
        Grass_Footstep      = 54,
        Grass_Footstep_2    = 55,
        Grass_Footstep_3    = 56,
        Grass_Footstep_4    = 57,
        Grass_Footstep_5    = 58,
        Grass_Footstep_6    = 59,
        Grass_Footstep_7    = 60,
        Grass_Footstep_8    = 61,
        High_Wild           = 62,
        Metal_Strike        = 63,
        Metal_Strike_2      = 64,
        Portal_Being_Hit    = 65,
        Portal_Being_Hit_2  = 66,
        Portal_Being_Hit_3  = 67,
        Portal_Break        = 68,
        Portal_Strike       = 69,
        Portal_Strike_2     = 70,
        Portal_Strike_3     = 71,
        Rushing_Wind_Boss   = 72,
        Shotgun             = 73,
        Shotgun_2           = 74,
        Shotgun_3           = 75,
        Shotgun_4           = 76,
        Stream              = 77,
        Sword_Shing         = 78,
        Sword_Shing_2       = 79,
        Sword_Shing_3       = 80,
        Sword_Shing_4       = 81
    };

    public enum LoopName
    {
        MainLoop = 0,
        Forest_Level_Loop = 1,
        TestMultiLoop = 2
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
			{ClipName.Stone_Enemy_Take_Damage_2, "Stone_Enemy_Take_Damage_2"},
            {ClipName.Dog_Attack, "Dog_Attack"},
            {ClipName.Dog_Attack_2, "Dog_Attack_2"},
            {ClipName.Dog_Attack_3, "Dog_Attack_3"},
            {ClipName.Dog_Death, "Dog_Death" },
            {ClipName.Dog_Death_2, "Dog_Death_2" },
            {ClipName.Dog_Death_3, "Dog_Death_3" },
            {ClipName.Dog_Growl, "Dog_Growl"},
            {ClipName.Dog_Pant, "Dog_Pant"},
            {ClipName.Electric_Short, "Electric_Short" },
            {ClipName.Electric_Short_2, "Electric_Short_2" },
            {ClipName.Electric_Short_3, "Electric_Short_3" },
            {ClipName.Forest_Ambience, "Forest_Ambience"},
            {ClipName.Grass_Footstep, "Grass_Footstep" },
            {ClipName.Grass_Footstep_2, "Grass_Footstep_2" },
            {ClipName.Grass_Footstep_3, "Grass_Footstep_3" },
            {ClipName.Grass_Footstep_4, "Grass_Footstep_4" },
            {ClipName.Grass_Footstep_5, "Grass_Footstep_5" },
            {ClipName.Grass_Footstep_6, "Grass_Footstep_6" },
            {ClipName.Grass_Footstep_7, "Grass_Footstep_7" },
            {ClipName.Grass_Footstep_8, "Grass_Footstep_8" },
            {ClipName.High_Wild, "High_Wild" },
            {ClipName.Metal_Strike, "Metal_Strike" },
            {ClipName.Metal_Strike_2, "Metal_Strike_2" },
            {ClipName.Portal_Being_Hit, "Portal_Being_Hit" },
            {ClipName.Portal_Being_Hit_2, "Portal_Being_Hit_2" },
            {ClipName.Portal_Being_Hit_3, "Portal_Being_Hit_3" },
            {ClipName.Portal_Break, "Portal_Break" },
            {ClipName.Portal_Strike, "Portal_Strike" },
            {ClipName.Portal_Strike_2, "Portal_Strike_2" },
            {ClipName.Portal_Strike_3, "Portal_Strike_3" },
            {ClipName.Rushing_Wind_Boss, "Rushing_Wind_Boss" },
            {ClipName.Shotgun, "Shotgun" },
            {ClipName.Shotgun_2, "Shotgun_2" },
            {ClipName.Shotgun_3, "Shotgun_3" },
            {ClipName.Shotgun_4, "Shotgun_4" },
            {ClipName.Stream, "Stream" },
            {ClipName.Sword_Shing, "Sword_Shing" },
            {ClipName.Sword_Shing_2, "Sword_Shing_2" },
            {ClipName.Sword_Shing_3, "Sword_Shing_3" },
            {ClipName.Sword_Shing_4, "Sword_Shing_4" }
        };

        private static readonly Dictionary<CueName, string> AudioCueNames = new Dictionary<CueName, string>()
        {
            {CueName.Random1, "Random1"},
            {CueName.Parallel1, "Parallel1"},
            {CueName.Forest_Loop_Light, "Forest_Loop_Light"},
            {CueName.Forest_Loop_Medium, "Forest_Loop_Medium"},
            {CueName.Forest_Loop_Heavy, "Forest_Loop_Heavy"},
            {CueName.Warp_In_Random, "Warp_In_Random"}
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
            List<ClipName> warpInClipsClipNames = new List<ClipName> 
            { 
                ClipName.Warp_In,
                ClipName.Warp_In_2,
                ClipName.Warp_In_3,
                ClipName.Warp_In_4,
                ClipName.Warp_In_5
            };
            AudioManager.Instance.CreateMultiCueRandom(CueName.Warp_In_Random, warpInClipsClipNames);

            List<ClipName> levelLoopIntensities = new List<ClipName>
            {
                ClipName.Forest_Level_Basic,
                ClipName.Forest_Level_Medium,
                ClipName.Forest_Level_Hard_Full
            };
            AudioManager.Instance.CreateLevelLoop(LoopName.TestMultiLoop, levelLoopIntensities);
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
