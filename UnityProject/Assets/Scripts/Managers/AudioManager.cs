using UnityEngine;
using System.Collections;
using Assets.Scripts.Constants;
using System.Collections.Generic;

namespace Assets.Scripts.Managers
{
    [AddComponentMenu("Manager/AudioManager")]
    [ExecuteInEditMode]
    public class AudioManager : MonoBehaviour
    {
        [SerializeField]
        private List<AudioClip> clips;

        private Dictionary<string, AudioClip> _soundCueList;
        private Dictionary<string, AudioSource> _loopCueList;
        private Dictionary<string, MultiCue> _multiCueList;

        private static AudioManager _instance;

        public static AudioManager Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = Object.FindObjectOfType<AudioManager>();
                    DontDestroyOnLoad(_instance.gameObject);
                }
                return _instance;
            }
        }

        void Awake()
        {

        }
        void Start()
        {
            _soundCueList = new Dictionary<string, AudioClip>();
            _loopCueList = new Dictionary<string, AudioSource>();
            _multiCueList = new Dictionary<string, MultiCue>();

            foreach (AudioClip clip in clips)
            {
                _soundCueList[clip.name] = clip;
            }
        }

        public void UpdateManager()
        {
            foreach(Transform c in transform)
                DestroyImmediate(c.gameObject);

            clips = new List<AudioClip>();

            // Load each clip into the list
            foreach (var clip in Resources.LoadAll<AudioClip>("Arts/Music"))
                clips.Add(clip);
        }

        public void DeleteClips()
        {
            clips = new List<AudioClip>();
        }


        // Creates a empty game object at sourceObjects world position and plays the sound name until sound is over
        // TODO: change to use enums
        public bool playCue(string name, GameObject sourceObject = null, float volume = 1.0f)
        {
            // See if we have this name mapped
            if (!_soundCueList.ContainsKey(name))
            {
                //play noise to indicate file was not found???
                Debug.Log("AudioManager:playCue - Unable to locate AudioClip >>"+name+"<<\n" );
                return false;
            }

            // Play at desired location if given, else play at the AudioManager GameObjects' Location
            Vector3 playAt = sourceObject ? sourceObject.transform.position : this.transform.position;
            
            AudioSource.PlayClipAtPoint(_soundCueList[name], playAt, volume);

            return true;
        }

        public bool playCueDelayed(string name, float delayTime, GameObject sourceObject = null, float volume = 1.0f)
        {
            if (!_soundCueList.ContainsKey(name))
            {
                Debug.Log("AudioManager::PlayCueDelayed - Unable to locate AudioClip >>" + name + "<<\n");
                return false;
            }
            AudioSource s = this.gameObject.AddComponent<AudioSource>();
            s.clip = findClip(name);
            s.loop = false;

            s.PlayDelayed(delayTime);
            
            return true;
        }


        //////////////////////////////////////////////
        // MULTICUE AUDIOCLIPS
        //////////////////////////////////////////////
        // Creates a empty game object at sourceObjects world position and plays a random sound in the List.
        // TODO: change to use enums??
        public bool playMultiCue(string name, GameObject sourceObject = null, float volume = 1.0f)
        {
            // See if we have this name mapped
            if (!_multiCueList.ContainsKey(name))
                return false;

            _multiCueList[name].play(sourceObject);

            return true;
        }


        // Creates mapping for one sound cue with multiple different output sounds that are randomized
        // TODO: change to use enums??
        public bool createMultiCueRandom(string name, List<KeyValuePair<string,int>> cueWeightList)
        {
            // See if a multi cue is already made with this name
            if (_multiCueList.ContainsKey(name))
                return false;

            MultiCue mCue = new MultiCue(cueWeightList);
            _multiCueList.Add(name, mCue);

            return true;
        }

        public bool createMultiCueParallel(string name, List<string> cueList)
        {
            if (_multiCueList.ContainsKey(name))
                return false;

            MultiCue mCue = new MultiCue(cueList);
            _multiCueList.Add(name, mCue);
            return true;
        }

        public bool createMultiCueSequential(string name, Dictionary<int, string> seqList)
        {
            if (_multiCueList.ContainsKey(name))
                return false;

            MultiCue mCue = new MultiCue(seqList);
            _multiCueList.Add(name, mCue);
            return true;
        }



        //////////////////////////////////////////////
        // LOOPING AUDIOCLIPS
        //////////////////////////////////////////////
        // Creates an audiosource and loops clip name
        // TODO: change to use enums
        public bool playLoop(string name, float volume = 1.0f)
        {
            createLoop(name);

            if (!_loopCueList.ContainsKey(name))
                return false;

            AudioSource loopSource = _loopCueList[name];
            loopSource.volume = volume;
            loopSource.Play();

            return true;
        }

        // Pauses audiosource for clip name
        // TODO: change to use enums
        public bool pauseLoop(string name)
        {
            if (!_loopCueList.ContainsKey(name))
                return false;

            _loopCueList[name].Pause();
            return true;
        }
        

        // Stops audiosource for clip name
        // TODO: change to use enums
        public bool stopLoop(string name)
        {
            if (!_loopCueList.ContainsKey(name))
                return false;

            _loopCueList[name].Stop();
            _loopCueList.Remove(name);
            AudioSource[] sources = this.gameObject.GetComponents<AudioSource>();
            foreach (var source in sources)
                if (source.clip == findClip(name))
                    Object.Destroy(source);

            return true;
        }

        // Creates AudioSource that has the clip name attached to it set to loop.
        // TODO: change to use enums
        private bool createLoop(string name)
        {
            // See if a multi cue is already made with this name
            if (_loopCueList.ContainsKey(name))
                return false;

            AudioClip clip = findClip(name);
            if (!clip)
                return false;
            
            AudioSource s = this.gameObject.AddComponent<AudioSource>();
            s.clip = clip;
            s.loop = true;

            _loopCueList.Add(name, s);

            return true;
        }



        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        // HELPER FUNCTIONS
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        // returns audio clip associated to this string
        // TODO: change to use enums
        public AudioClip findClip(string name)
        {
            if (_soundCueList != null)
                if (_soundCueList.ContainsKey(name))
                    return _soundCueList[name];
            return null;
        }




        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        // HELPER CLASSES
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        enum MultiCueType { Sequential, Parallel, Weighted };


        /// MultiCue Helper class
        public class MultiCue
        {
            // for weighted
            List<KeyValuePair<string, int>> cueWeightList;
            int totalWeight;

            // for parallel
            List<string> cueList;

            // for sequential
            Dictionary<int,string> seqList;
            int totalSequences;
            int currentSeq;

            float volume;
            MultiCueType type;

            public MultiCue(List<KeyValuePair<string,int>> inputList)
            {
                type = MultiCueType.Weighted;
                volume = 1.0f;
                cueWeightList = new List<KeyValuePair<string, int>>();
                totalWeight = 0;
                for (int i = 0; i < inputList.Count; i++)
                {
                    KeyValuePair<string, int> newPair = new KeyValuePair<string, int>(inputList[i].Key, inputList[i].Value + totalWeight);
                    cueWeightList.Add(newPair);
                    totalWeight += inputList[i].Value;
                }
            }
            public MultiCue(List<string> cueList)
            {
                type = MultiCueType.Parallel;
                this.cueList = cueList;
                volume = 1.0f;
            }
            public MultiCue(Dictionary<int,string> seqList)
            {
                type = MultiCueType.Sequential;
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
                
                if (type == MultiCueType.Weighted)
                {
                    if (cueWeightList.Count == 0)
                        return false;

                    int weightToPlay = Random.Range(0, totalWeight);

                    for (int i = 0; i < cueWeightList.Count; i++)
                        if (weightToPlay < cueWeightList[i].Value)
                            return AudioManager.Instance.playCue(cueWeightList[i].Key, sourceObj, volume);
                }
                else if(type == MultiCueType.Parallel)
                {
                    if (cueList.Count == 0)
                        return false;
                    foreach (var audioStr in cueList)
                        AudioManager.Instance.playCue(audioStr, sourceObj, volume);
                }
                else if(type == MultiCueType.Sequential)
                {
                    if (seqList.Count == 0)
                        return false;
                    float totalTime = 0.0f;

                    int curSeq = 0;
                    while(curSeq != totalSequences)
                    {
                        AudioManager.Instance.playCueDelayed(seqList[curSeq],totalTime,sourceObj,volume);
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