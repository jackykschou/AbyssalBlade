using System.Collections.Generic;
using System;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Constants
{
    public enum CueName
    {
        ExampleLoop,
        Random1
    };

    public enum ClipName
    {
        Strike,
        Laser,
        Laser1
    };

    public enum MultiCueType 
    { 
        Sequential, 
        Parallel, 
        Random 
    };

    public class AudioConstants
    {
        public const string AudioExtension = ".mp3";
        public const string StartingAssetAudioPath = "Assets/Resources/Arts/Music/Cues/";

        private static readonly Dictionary<ClipName, string> AudioClipNames = new Dictionary<ClipName, string>()
        {
            {ClipName.Strike, "strike"},
            {ClipName.Laser, "Laser"},
            {ClipName.Laser1, "Laser1"}
        };

        private static readonly Dictionary<CueName, string> AudioCueNames = new Dictionary<CueName, string>()
        {
            {CueName.ExampleLoop, "TestLoop"},
            {CueName.Random1, "Random1"}
        };

        public AudioConstants()
        {
            CreateCustomCues();
        }
        void CreateCustomCues()
        {
            AudioManager.LoopingCue loop = new AudioManager.LoopingCue(AudioCueNames[CueName.ExampleLoop]);
            loop.addTrack(AudioManager.Instance.findClip(AudioClipNames[ClipName.Strike]));
            loop.addTrack(AudioManager.Instance.findClip(AudioClipNames[ClipName.Laser]));
            AudioManager.Instance.createLoop(loop);
        }

        public static string GetCueName(CueName name)
        {
            if(!AudioCueNames.ContainsKey(name))
                throw new Exception("Prefab is not defined");
            return AudioCueNames[name];
        }
        public static string GetClipName(ClipName name)
        {
            if (!AudioClipNames.ContainsKey(name))
                throw new Exception("Prefab is not defined");
            return AudioClipNames[name];
        }

    }
}
