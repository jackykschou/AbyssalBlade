using System.Collections.Generic;

namespace Assets.Scripts.Constants
{
    public enum CueNames
    {
        
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
    }
}
