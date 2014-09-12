using UnityEngine;
using System.Collections;
using Assets.Scripts.Constants;
using System.Collections.Generic;

namespace Assets.Scripts.Managers
{
    public class AudioManager
    {
        private Dictionary<string, AudioClip> _soundCueList;

        private static readonly AudioManager _instance = new AudioManager();

        public static AudioManager Instance { get { return _instance; } }


        AudioManager()
        {
            // Create list to play from
            _soundCueList = new Dictionary<string, AudioClip>();

            // Load each clip into the list
            foreach (var clip in Resources.LoadAll<AudioClip>("Arts/Music"))
               _soundCueList.Add(clip.name, clip);
        }


        public bool playCue(string name, GameObject sourceObject, float volume = 1.0f)
        {
            // Non Null Object to play the cue on
            if (!sourceObject)
                return false;

            // See if we have this name mapped
            if (!_soundCueList.ContainsKey(name))
            {
                //play noise to indicate file was not found???
                return false;
            }

            AudioSource.PlayClipAtPoint(_soundCueList[name], sourceObject.transform.position,volume);

            return true;
        }
    }
}