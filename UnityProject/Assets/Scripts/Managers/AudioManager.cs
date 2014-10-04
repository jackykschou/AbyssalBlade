using System;
using System.Collections;
using Assets.Scripts.Constants;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Utility;

namespace Assets.Scripts.Managers
{
    [AddComponentMenu("Manager/AudioManager")]
    [ExecuteInEditMode]
    public class AudioManager : MonoBehaviour
    {
        public int NumAudioSources;
        [SerializeField]
        private List<AudioClip> _clips;
        [SerializeField]
        private List<MultiCue> _cues;
        [SerializeField]
        private List<LoopingCue> _loops;

        private Dictionary<string, AudioClip> _oneShotList;
        private Dictionary<CueName, MultiCue> _cueDict;
        private Dictionary<LoopName, LoopingCue> _loopDict;

        private List<AudioSource> _sources;
        GameObject sourceHolder;

        int NextSourceIndex;

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
        }
        void Update()
        {
            if(_loops != null)
                foreach (var loop in _loops)
                    loop.Update();
        }
        public void UpdateManager()
        {
            DeleteClips();
            NextSourceIndex = 0;
            _sources = new List<AudioSource>();
            _clips = new List<AudioClip>();
            _cues = new List<MultiCue>();
            _loops = new List<LoopingCue>();
            _oneShotList = new Dictionary<string, AudioClip>();
            _cueDict = new Dictionary<CueName, MultiCue>();
            _loopDict = new Dictionary<LoopName, LoopingCue>();

            foreach (var clip in Resources.LoadAll<AudioClip>("Arts/Music"))
                _clips.Add(clip);

            foreach (AudioClip clip in _clips)
                _oneShotList[clip.name] = clip;

            AudioConstants.CreateCustomCues();

            foreach (MultiCue cue in _cues)
                _cueDict[cue.cueName] = cue;

            sourceHolder = new GameObject("SourceHolder");
            sourceHolder.transform.parent = this.gameObject.transform;

            for (int i = 0; i < NumAudioSources; i++)
                _sources.Add(sourceHolder.AddComponent<AudioSource>());
                
        }
        public void DeleteClips()
        {
            _sources = new List<AudioSource>();
            _clips = new List<AudioClip>();
            _cues = new List<MultiCue>();
            _loops = new List<LoopingCue>();
            _oneShotList = new Dictionary<string, AudioClip>();
            _cueDict = new Dictionary<CueName, MultiCue>();
            _loopDict = new Dictionary<LoopName, LoopingCue>();
            DestroyImmediate(GameObject.Find("SourceHolder"));
        }
        //////////////////////////////////////////////
        // SINGLE AUDIOCLIPS
        //////////////////////////////////////////////
        public bool playClip(ClipName name, GameObject sourceObject = null, float volume = 1.0f)
        {
            string clipName = AudioConstants.GetClipName(name);
            if (!_oneShotList.ContainsKey(clipName))
            {
                Debug.Log("Cannot find the Clip >>" + name + "<<\n");
                return false;
            }

            AudioSource s = getNextAvailableSource();
            if(s == null)
            {
                Debug.Log("Cannot find an AudioSource for " + name);
                return false;
            }
            s.volume = volume;
            s.clip = findClip(name);
            s.Play();

            return true;
        }

        public bool playClipDelayed(ClipName name, float delayTime, GameObject sourceObject = null, float volume = 1.0f)
        {
            string clipName = AudioConstants.GetClipName(name);
            if (!_oneShotList.ContainsKey(clipName))
                return false;
            GameObject obj = sourceObject ? sourceObject.gameObject : this.gameObject;
            AudioSource s = obj.AddComponent<AudioSource>();
            s.clip = findClip(name);
            s.loop = false;
            s.volume = volume;
            s.PlayDelayed(delayTime);
            StartCoroutine(this.TimedDespawn(s,delayTime+s.clip.length));
            return true;
        }
        //////////////////////////////////////////////
        // MULTI CUES
        //////////////////////////////////////////////
        public bool playCue(CueName name, GameObject sourceObject = null, float volume = 1.0f)
        {
            if(_cueDict.ContainsKey(name))
                _cueDict[name].play();
            return true;
        }
        //////////////////////////////////////////////
        // LOOPING CUES
        //////////////////////////////////////////////
        public bool playLoop(LoopName name, float volume = 1.0f)
        {
            if (!_loopDict.ContainsKey(name))
                return false;
            if (_loopDict[name].running)
                return false;
            _loopDict[name].volume = volume;
            _loopDict[name].Play();
            return true;
        }
        public bool swapLoopTrack(LoopName name)
        {
            if (!_loopDict.ContainsKey(name))
                return false;
            _loopDict[name].switchTrack();

            return true;
        }
        public bool stopLoop(LoopName name)
        {
            if (!_loopDict.ContainsKey(name))
                return false;
            _loopDict[name].Stop();
            return true;
        }
        //////////////////////////////////////////////
        // Creation Methods
        //////////////////////////////////////////////
        public bool createMultiCueRandom(CueName name, List<ClipName> clipList)
        {
            List<ProportionValue<ClipName>> list = new List<ProportionValue<ClipName>>();
            foreach( var clip in clipList)
                list.Add(ProportionValue.Create(1.0f / clipList.Count, clip));
            _cues.Add(new MultiCue(name, list));
            return true;
        }
        public bool createMultiCueParallel(CueName name, List<ClipName> clipList)
        {
            _cues.Add(new MultiCue(name, clipList));
            return true;
        }
        public bool createLoop(LoopName name, List<ClipName> clipList, float volume = 1.0f)
        {
            if (_loopDict.ContainsKey(name))
                return false;

            if (_instance != null)
            {
                LoopingCue cue = new LoopingCue(name);
                cue.clips = clipList;
                cue.volume = volume;
                cue.name = AudioConstants.GetLoopName(name);
                _loopDict[name] = cue;
                _loops.Add(cue);
            }
            return true;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        // HELPER FUNCTIONS
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        public AudioClip findClip(ClipName name)
        {
            string clipName = AudioConstants.GetClipName(name);
            if (_oneShotList.ContainsKey(clipName))
                return _oneShotList[clipName];
            return null;
        }
        private void DestroySource(AudioSource _source)
        {
            DestroyImmediate(_source);
        }
        private IEnumerator TimedDespawn(AudioSource s, float time)
        {
            yield return new WaitForSeconds(time);
            DestroyImmediate(s);
        }
        private AudioSource getNextAvailableSource()
        {
            if (NextSourceIndex >= NumAudioSources)
                NextSourceIndex = 0;
            if (_sources[NextSourceIndex].isPlaying)
                _sources[NextSourceIndex].Stop();
            return _sources[NextSourceIndex++];
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        // HELPER CLASSES
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        public class LoopingCue
        {
            [HideInInspector]
            public string name;
            public LoopName loopName;
            public bool running = false;
            public int _curTrack;
            [Range(0.0f, 1.0f)]
            public float volume = 1.0f;
            public List<ClipName> clips;
            List<AudioSource> audioSources;
            private double nextEventTime;

            public LoopingCue(LoopName name1)
            {
                this.name = AudioConstants.GetLoopName(name1);
                audioSources = new List<AudioSource>();
            }
            public void Play()
            {
                for (int i = 0; i < clips.Count; i++)
                    audioSources.Add(AudioManager.Instance.gameObject.AddComponent("AudioSource") as AudioSource);
                nextEventTime = AudioSettings.dspTime;
                running = true;
            }
            public void Stop()
            {
                running = false;
                foreach (var source in audioSources)
                    source.Stop();
            }
            public void switchTrack()
            {
                if (_curTrack == clips.Count - 1)
                    _curTrack = 0;
                else
                    _curTrack++;
            }
            public void Update()
            {
                if (!running)
                    return;

                // Perfectly In Sync with Unity Audio System
                double time = AudioSettings.dspTime;

                if (time > nextEventTime)
                {
                    audioSources[_curTrack].clip = AudioManager.Instance.findClip(clips[_curTrack]);
                    audioSources[_curTrack].volume = volume;
                    audioSources[_curTrack].Play();
                    nextEventTime += audioSources[_curTrack].clip.length;
                }
            }
        }
        public enum MultiCueType
        {
            Parallel,
            Random
        };
        [Serializable]
        public class MultiCue
        {
            [HideInInspector]
            public string name;
            [HideInInspector]
            public CueName cueName;
            private MultiCueType CueType;
            [Range(0, 1.0f)]
            public float volume;
            [SerializeField]
            List<ClipName> clips; // List of all Clips
            ProportionValue<ClipName>[] cueWeightProportions; // Percentage based Cue List
            int totalWeight;

            public MultiCueType type
            {
                get
                {
                    return CueType;
                }
            }

            // Random MultiCue
            public MultiCue(CueName name, List<ProportionValue<ClipName>> list)
            {
                this.cueName = name;
                this.name = AudioConstants.GetCueName(name);
                clips = new List<ClipName>();
                foreach (var pv in list)
                    clips.Add(pv.Value);
                CueType = MultiCueType.Random;
                volume = 1.0f;
                cueWeightProportions = list.ToArray();
            }
            // Parallel MultiCue
            public MultiCue(CueName name, List<ClipName> clipNameList)
            {
                this.cueName = name;
                this.name = AudioConstants.GetCueName(name);
                clips = clipNameList;
                CueType = MultiCueType.Parallel;
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
                    ClipName c = ProportionValue.ChooseByRandom<ClipName>(cueWeightProportions);
                    return AudioManager.Instance.playClip(c, obj, volume);
                }
                else if (CueType == MultiCueType.Parallel)
                {
                    if (clips.Count == 0)
                        return false;
                    foreach (var clip in clips)
                        AudioManager.Instance.playClip(clip, obj, volume);
                    return true;
                }
                return false;
            }

            // sets the volume this multiCue will be played at
            public bool setVolume(float f)
            {
                if (f < 0 || f > 1)
                    return false;
                volume = f;
                return true;
            }
        }
    }
}