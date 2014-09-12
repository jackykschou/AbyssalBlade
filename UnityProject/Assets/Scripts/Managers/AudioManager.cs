using UnityEngine;
using System.Collections;
using Assets.Scripts.Constants;
using System.Collections.Generic;

namespace Assets.Scripts.Managers
{
    public class AudioManager
    {
        private Dictionary<string, AudioClip> _soundCueList;
        private Dictionary<string, List<AudioClip>> _multiCueList;

        private static readonly AudioManager _instance = new AudioManager();

        public static AudioManager Instance { get { return _instance; } }


        AudioManager()
        {
            // Create list to play from
            _soundCueList = new Dictionary<string, AudioClip>();
            _multiCueList = new Dictionary<string, List<AudioClip>>();

            // Load each clip into the list
            foreach (var clip in Resources.LoadAll<AudioClip>("Arts/Music"))
               _soundCueList.Add(clip.name, clip);
        }


        // Creates a empty game object at sourceObjects world position and plays the sound name until sound is over
        public bool playCue(string name, GameObject sourceObject, float volume = 1.0f)
        {
            // See if we have this name mapped
            if (!_soundCueList.ContainsKey(name))
            {
                //play noise to indicate file was not found???
                return false;
            }

            AudioSource.PlayClipAtPoint(_soundCueList[name], sourceObject.transform.position,volume);

            return true;
        }

        // Creates mapping for one sound cue with multiple different output sounds that are randomized
        // TODO: add Weights to the sound cue names
        public bool createMultiCue(string name, string[] cueNames)
        {
            // See if a multi cue is already made with this name
            if (_multiCueList.ContainsKey(name))
                return false;

            List<AudioClip> clips = new List<AudioClip>();
            foreach (var cueName in cueNames)
            {
                AudioClip clip = findClip(cueName);
                if (!clip)
                    return false;
                clips.Add(clip);
            }

            _multiCueList.Add(name, clips);

            return true;
        }

        // Plays multi cue previously set up with the AudioManager
        public bool playMultiCue(string name, GameObject sourceObject, float volume = 1.0f)
        {
            if (!_multiCueList.ContainsKey(name))
                return false;

            AudioSource.PlayClipAtPoint(_multiCueList[name][Random.Range(0, _multiCueList.Count)], sourceObject.transform.position, volume);

            return true;
        }

        private AudioClip findClip(string name)
        {
            return _soundCueList[name];
        }
    }
}