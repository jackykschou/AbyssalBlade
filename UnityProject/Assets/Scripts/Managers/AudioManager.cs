#define TESTING
using System;
using System.Linq;
using System.Collections;
using Assets.Scripts.Constants;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Assets.Scripts.Managers
{
    [AddComponentMenu("Manager/AudioManager")]
    [ExecuteInEditMode]
    public class AudioManager : MonoBehaviour
    {
        [SerializeField]
        private List<AudioClip> clips;
        [SerializeField]
        private List<MultiCue> cues;

        private Dictionary<string, AudioClip> _oneShotList;
        private Dictionary<string, MultiCue> _parallelList;
        private Dictionary<string, MultiCue> _sequentialList;
        private Dictionary<string, MultiCue> _weightedList;

        private static AudioManager _instance;

        public static AudioManager Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = (AudioManager)UnityEngine.Object.FindObjectOfType<AudioManager>();
                    DontDestroyOnLoad(_instance.gameObject);
                }
                return _instance;
            }
        }
        void Awake()
        {
            UpdateManager();

            foreach(var clip in clips)
                _oneShotList[clip.name] = clip;

            foreach (var cue in cues)
            {
                switch (cue.type)
                {
                    case MultiCueType.Parallel:
                        _parallelList[cue.name] = cue;
                        break;
                    case MultiCueType.Sequential:
                        _sequentialList[cue.name] = cue;
                        break;
                    case MultiCueType.Random:
                        _weightedList[cue.name] = cue;
                        break;
                    default:

                        break;
                }
            }
        }

        public void UpdateManager()
        {
            clips = new List<AudioClip>();
            //cues = new List<MultiCue>();
            _oneShotList = new Dictionary<string, AudioClip>();
            _parallelList = new Dictionary<string, MultiCue>();
            _sequentialList = new Dictionary<string, MultiCue>();
            _weightedList = new Dictionary<string, MultiCue>();

            foreach (var clip in Resources.LoadAll<AudioClip>("Arts/Music"))
            {
                clips.Add(clip);
                if (_oneShotList != null)
                    _oneShotList[clip.name] = clip;
            }
            
            DirectoryInfo dir = new DirectoryInfo(AudioConstants.StartingAssetAudioPath);
            DirectoryInfo[] subDirectories = dir.GetDirectories();
            foreach (var d in subDirectories)
            {
                if (d.Name.Equals("Parallel"))
                {
                    foreach (var sd in d.GetDirectories())
                    {
                        List<AudioClip> clipList = new List<AudioClip>();
                        string cuenames = "";
                        foreach (var clip in Resources.LoadAll<AudioClip>("Arts/Music/Cues/Parallel/" + sd.Name))
                        {
                            clipList.Add(clip);
                            cuenames += "(" + clip.name + ") ";
                        }
                        Debug.Log("Creating Parallel MultiCue: " + cuenames);
                        createMultiCueParallel(sd.Name,clipList);
                    }
                }
                else if (d.Name.Equals("Random"))
                {
                    foreach (var sd in d.GetDirectories())
                    {
                        List<AudioClip> clipList = new List<AudioClip>();
                        string cuenames = "";
                        foreach (var clip in Resources.LoadAll<AudioClip>("Arts/Music/Cues/Random/" + sd.Name))
                        {
                            clipList.Add(clip);
                            cuenames += "(" + clip.name + ") ";
                        }
                        Debug.Log("Creating Random MultiCue: " + cuenames);
                        createMultiCueRandom(sd.Name, clipList);
                    }
                }
            }            
        }

        public void DeleteClips()
        {
            clips = new List<AudioClip>();
            cues = new List<MultiCue>();
            _oneShotList = new Dictionary<string, AudioClip>();
            _parallelList = new Dictionary<string, MultiCue>();
            _sequentialList = new Dictionary<string, MultiCue>();
            _weightedList = new Dictionary<string, MultiCue>();
        }

        public bool playClip(string name, GameObject sourceObject = null, float volume = 1.0f)
        {
            if (!_oneShotList.ContainsKey(name))
            {
                Debug.Log("AudioManager:playCue - Unable to locate AudioClip >>"+name+"<<\n" );
                return false;
            }

            Vector3 playAt = sourceObject ? sourceObject.transform.position : this.transform.position;
            
            AudioSource.PlayClipAtPoint(findClip(name), playAt, volume);

            return true;
        }
        public bool playClipDelayed(string name, float delayTime, GameObject sourceObject = null, float volume = 1.0f)
        {
            /*
            if (!_soundCueList.ContainsKey(name))
            {
                Debug.Log("AudioManager::PlayCueDelayed - Unable to locate AudioClip >>" + name + "<<\n");
                return false;
            }
            AudioSource s = this.gameObject.AddComponent<AudioSource>();
            s.clip = findClip(name);
            s.loop = false;

            s.PlayDelayed(delayTime);
            */
            return true;
        }

        public bool playCue(string name, GameObject sourceObject = null, float volume = 1.0f)
        {
            bool found = false;

            foreach (var cue in cues)
                if (cue.name.Equals(name))
                    found = true;

            if (!found)
                return false;
            if (_parallelList.ContainsKey(name))
                _parallelList[name].play(sourceObject);
            else if (_weightedList.ContainsKey(name))
                _weightedList[name].play(sourceObject);

            return true;
        }

        public bool createMultiCueRandom(string name, List<KeyValuePair<string,int>> cueWeightList)
        {
           // if (_multiCueList.ContainsKey(name))
             //   return false;

            //MultiCue mCue = new MultiCue(name,cueWeightList);
            //_multiCueList.Add(name, mCue);

            return true;
        }
        public bool createMultiCueRandom(string name, List<AudioClip> clipList)
        {
            foreach (MultiCue cue in cues)
                if (cue.name.Equals(name))
                    return false;

            List<KeyValuePair<string, int>> weightList = new List<KeyValuePair<string, int>>();
            foreach(AudioClip clip in clipList)
            {
                KeyValuePair<string, int> keyValuePair = new KeyValuePair<string, int>(clip.name,1);
                weightList.Add(keyValuePair);
            }
            MultiCue mCue = new MultiCue(name, weightList);
            cues.Add(mCue);
            return true;
        }
        public bool createMultiCueParallel(string name, List<string> cueList)
        {
            //if (_parallelList.ContainsKey(name))
            //    return false;
            /*
            foreach (MultiCue cue in cues)
                if (cue.name.Equals(name))
                    return false;
            MultiCue mCue = new MultiCue(name,cueList);
            if(_parallelList != null)
                _parallelList.Add(name, mCue);
            cues.Add(mCue);
            */return true;
        }
        public bool createMultiCueParallel(string name, List<AudioClip> clipList)
        {
            foreach (MultiCue cue in cues)
                if (cue.name.Equals(name))
                    return false;

            MultiCue mCue = new MultiCue(name, clipList);

            cues.Add(mCue);
            return true;
        }
        public bool createMultiCueSequential(string name, Dictionary<int, string> seqList)
        {
           // if (_multiCueList.ContainsKey(name))
                return false;

            //MultiCue mCue = new MultiCue(name, seqList);
            //_multiCueList.Add(name, mCue);
            //return true;
        }

        //////////////////////////////////////////////
        // LOOPING AUDIOCLIPS
        //////////////////////////////////////////////
        public bool playLoop(string name, float volume = 1.0f)
        {
           //createLoop(name);

            //if (!_loopCueList.ContainsKey(name))
            //    return false;

            //AudioSource loopSource = _loopCueList[name];
           // loopSource.volume = volume;
           // loopSource.Play();

            return true;
        }
        public bool pauseLoop(string name)
        {
            //if (!_loopCueList.ContainsKey(name))
            //    return false;

            //_loopCueList[name].Pause();
            return true;
        }      
        public bool stopLoop(string name)
        {
            //if (!_loopCueList.ContainsKey(name))
                return false;

           // _loopCueList[name].Stop();
           // _loopCueList.Remove(name);
            //AudioSource[] sources = this.gameObject.GetComponents<AudioSource>();
            //foreach (var source in sources)
           //     if (source.clip == findClip(name))
           //         UnityEngine.Object.Destroy(source);

           // return true;
        }

        // Creates AudioSource that has the clip name attached to it set to loop.
        // TODO: change to use enums
        private bool createLoop(string name)
        {
            // See if a multi cue is already made with this name
            //if (_loopCueList.ContainsKey(name))
                return false;

            //AudioClip clip = findClip(name);
            //if (!clip)
           //     return false;
            
          //  AudioSource s = this.gameObject.AddComponent<AudioSource>();
          //  s.clip = clip;
          //  s.loop = true;

            //_loopCueList.Add(name, s);

           // return true;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        // HELPER FUNCTIONS
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        // returns audio clip associated to this string
        // TODO: change to use enums
        public AudioClip findClip(string name)
        {
            //foreach (AudioClip clip in clips)
            //    if (clip.name.Equals(name))
           //         return clip;
            if (_oneShotList.ContainsKey(name))
                return _oneShotList[name];
            return null;
        }
        
        [Serializable]
        public class MultiCue
        {
            public string name;

            [SerializeField]
            private MultiCueType CueType;

            [Range(0, 1.0f)]
            public float volume;

            [SerializeField]
            List<AudioClip> clips;

            public MultiCueType type
            {
                get
                {
                    return CueType;
                }
            }

            // for weighted
            List<KeyValuePair<string, int>> cueWeightList;
            int totalWeight;


            // for sequential
            Dictionary<int,string> seqList;
            int totalSequences;
            int currentSeq;


            public MultiCue(string name,List<KeyValuePair<string,int>> inputList)
            {
                this.name = name;
                clips = new List<AudioClip>();
                CueType = MultiCueType.Random;
                volume = 1.0f;
                cueWeightList = new List<KeyValuePair<string, int>>();
                totalWeight = 0;
                for (int i = 0; i < inputList.Count; i++)
                {
                    KeyValuePair<string, int> newPair = new KeyValuePair<string, int>(inputList[i].Key, inputList[i].Value + totalWeight);
                    cueWeightList.Add(newPair);
                    totalWeight += inputList[i].Value;
                    clips.Add(AudioManager.Instance.findClip(inputList[i].Key));
                }
            }
            public MultiCue(string name, List<AudioClip> clipList)
            {
                this.name = name;
                clips = clipList;
                CueType = MultiCueType.Parallel;
                volume = 1.0f;
            }
            public MultiCue(string name, Dictionary<int, string> seqList)
            {
                this.name = name;
                clips = new List<AudioClip>();
                CueType = MultiCueType.Sequential;
                this.seqList = seqList;
                totalSequences = seqList.Count;
                volume = 1.0f;
            }
            // plays this MultiCue at sourceObj's world position
            public bool play(GameObject sourceObj = null)
            {
                GameObject obj = sourceObj
                    ? sourceObj
                    : AudioManager.Instance.gameObject;

                if (CueType == MultiCueType.Random)
                {
                    int weightToPlay = UnityEngine.Random.Range(0, totalWeight);

                    for (int i = 0; i < cueWeightList.Count; i++)
                        if (weightToPlay < cueWeightList[i].Value)
                            return AudioManager.Instance.playClip(cueWeightList[i].Key, obj, volume);
                }
                else if (CueType == MultiCueType.Parallel)
                {
                    if (clips.Count == 0)
                        return false;
                    foreach (var clip in clips)
                        AudioManager.Instance.playClip(clip.name, obj, volume);
                }
                else if (CueType == MultiCueType.Sequential)
                {
                    if (seqList.Count == 0)
                        return false;
                    float totalTime = 0.0f;

                    int curSeq = 0;
                    while(curSeq != totalSequences)
                    {
                        AudioManager.Instance.playClipDelayed(seqList[curSeq], totalTime, obj, volume);
                        totalTime += AudioManager.Instance.findClip(seqList[curSeq]).length;
                        curSeq++;
                    }

                }
                return true;
            }

            // sets the volume this multiCue will be played at
            public bool setVolume(float f)
            {
                if(f < 0 || f > 1)
                    return false;
                volume = f;
                return true;
            }
        }
    }
}