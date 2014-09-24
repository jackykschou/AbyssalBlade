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
        Dash,
        Footstep,
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
        Strike,
        Swipe
    };

    public enum LoopName
    {
        ExampleLoop
    };


    public class AudioConstants
    {
        public const string AudioExtension = ".mp3";
        public const string StartingAssetAudioPath = "Assets/Resources/Arts/Music/Cues/";

        private static readonly Dictionary<ClipName, string> AudioClipNames = new Dictionary<ClipName, string>()
        {
            {ClipName.Dash, "Dash"},
            {ClipName.Footstep, "Footstep"},
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
            {ClipName.Strike, "Strike"},
            {ClipName.Swipe, "Swipe"}
        };

        private static readonly Dictionary<CueName, string> AudioCueNames = new Dictionary<CueName, string>()
        {
            {CueName.Random1, "Random1"},
            {CueName.Parallel1, "Parallel1"}
        };


        private static readonly Dictionary<LoopName, string> AudioLoopNames = new Dictionary<LoopName, string>()
        {
            {LoopName.ExampleLoop, "ExampleLoop"}
        };

        public static void CreateCustomCues()
        {
            // Examples
            List<ClipName> loopList = new List<ClipName>();
            loopList.Add(ClipName.Strike);
            loopList.Add(ClipName.Laser);
            AudioManager.Instance.createLoop(LoopName.ExampleLoop, loopList);

            List<ClipName> randList = new List<ClipName>();
            randList.Add(ClipName.Shot);
            randList.Add(ClipName.Strike);
            randList.Add(ClipName.Swipe);
            AudioManager.Instance.createMultiCueRandom(CueName.Random1,randList);


            List<ClipName> parallelList = new List<ClipName>();
            randList.Add(ClipName.Swipe);
            randList.Add(ClipName.Strike);
            AudioManager.Instance.createMultiCueParallel(CueName.Parallel1, parallelList);

            return;
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
