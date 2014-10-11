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
        [SerializeField]
        private List<LevelLoopingCue> _levelLoops;

        private Dictionary<string, AudioClip> _oneShotList;
        private Dictionary<CueName, MultiCue> _cueDict;
        private Dictionary<LoopName, LoopingCue> _loopDict;
        private Dictionary<LoopName, LevelLoopingCue> _levelLoopDict; 

        private List<AudioSource> _sources;
        GameObject sourceHolder;

        Dictionary<string, int> _queuedClipDict;
        List<string> keys;

        int NextSourceIndex;

        private static AudioManager _instance;
        public static AudioManager Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = FindObjectOfType<AudioManager>();
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
            if (Input.GetKeyDown(KeyCode.Keypad5))
                Instance.playLevelLoop(LoopName.TestMultiLoop);
            if (Input.GetKeyDown(KeyCode.Keypad7))
                Instance.setLightIntensity(LoopName.TestMultiLoop);
            if (Input.GetKeyDown(KeyCode.Keypad8))
                Instance.setMediumIntensity(LoopName.TestMultiLoop);
            if (Input.GetKeyDown(KeyCode.Keypad9))
                Instance.setHeavyIntensity(LoopName.TestMultiLoop);

            if(_loops != null)
                foreach (var loop in _loops)
                    loop.Update();

            if (_queuedClipDict == null)
                return;

            foreach (var key in keys)
            {
                if (!_queuedClipDict.ContainsKey(key))
                    continue;
                if (_queuedClipDict[key] > 0)
                {
                    AudioSource s = getNextAvailableSource();
                    if (s == null)
                    {
                        Debug.Log("Cannot find an AudioSource for " + name);
                        return;
                    }
                    s.volume = 1.0f;
                    s.clip = _oneShotList[key];
                    s.Play();
                    _queuedClipDict[key] = 0;
                }
            }
        }
        public void UpdateManager()
        {
            DeleteClips();
            NextSourceIndex = 0;

            foreach (var clip in Resources.LoadAll<AudioClip>("Arts/Music"))
                _clips.Add(clip);

            foreach (AudioClip clip in _clips)
            {
                _oneShotList[clip.name] = clip;
                _queuedClipDict.Add(clip.name, 0);
            }

            AudioConstants.CreateCustomCues();

            foreach (MultiCue cue in _cues)
                _cueDict[cue.CueName] = cue;

            sourceHolder = new GameObject("SourceHolder");
            sourceHolder.transform.parent = this.gameObject.transform;

            for (int i = 0; i < NumAudioSources; i++)
                _sources.Add(sourceHolder.AddComponent<AudioSource>());

            keys = new List<string>(_queuedClipDict.Keys);
                        
        }
        public void DeleteClips()
        {
            _sources = new List<AudioSource>();
            _clips = new List<AudioClip>();
            _cues = new List<MultiCue>();
            _loops = new List<LoopingCue>();
            _levelLoops = new List<LevelLoopingCue>();
            _oneShotList = new Dictionary<string, AudioClip>();
            _cueDict = new Dictionary<CueName, MultiCue>();
            _loopDict = new Dictionary<LoopName, LoopingCue>();
            _levelLoopDict = new Dictionary<LoopName, LevelLoopingCue>();
            _queuedClipDict = new Dictionary<string, int>();
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

            _queuedClipDict[clipName]++;

            return true;
        }//////////////////////////////////////////////
        // SINGLE AUDIOCLIPS
        //////////////////////////////////////////////
        public bool playClip(ClipName name, AudioSource s)
        {
            string clipName = AudioConstants.GetClipName(name);
            if (!_oneShotList.ContainsKey(clipName))
            {
                Debug.Log("Cannot find the Clip >>" + name + "<<\n");
                return false;
            }
            s.clip = findClip(name);
            s.volume = 1.0f;
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
        public bool playCue(CueName name, AudioSource source)
        {
            if (_cueDict.ContainsKey(name))
                _cueDict[name].play(source);
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
        public bool playLevelLoop(LoopName name, float volume = 1.0f)
        {
            if (!_levelLoopDict.ContainsKey(name))
                return false;
            if (_levelLoopDict[name].running)
                return false;
            _levelLoopDict[name].volume = volume;
            _levelLoopDict[name].Play();
            return true;
        }
        public bool swapLoopTrack(LoopName name)
        {
            if (_loopDict.ContainsKey(name))
            {
                _loopDict[name].switchTrack();
                return true;
            }
            return false;
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
        public bool createLevelLoop(LoopName name, List<ClipName> clipList, float volume = 1.0f)
        {
            if (_levelLoopDict.ContainsKey(name))
                return false;

            if (_instance != null)
            {
                LevelLoopingCue cue = new LevelLoopingCue(name,clipList);
                cue.volume = volume;
                cue.name = AudioConstants.GetLoopName(name);
                _levelLoopDict[name] = cue;
                _levelLoops.Add(cue);
            }
            return true;
        }

        public void setMediumIntensity(LoopName name)
        {
            if (!_levelLoopDict.ContainsKey(name))
                return;

            _levelLoopDict[name].enableMedium();
        }
        public void setHeavyIntensity(LoopName name)
        {
            if (!_levelLoopDict.ContainsKey(name))
                return;

            _levelLoopDict[name].enableHeavy();
        }

        public void setLightIntensity(LoopName name)
        {
            if (!_levelLoopDict.ContainsKey(name))
                return;
            _levelLoopDict[name].enableLight();
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

        public MultiCue findCue(CueName name)
        {
            if (_cueDict.ContainsKey(name))
                return _cueDict[name];
            return null;
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

            public LoopingCue(LoopName name)
            {
                this.name = AudioConstants.GetLoopName(name);
                audioSources = new List<AudioSource>();
            }
            public void Play()
            {
                for (int i = 0; i < clips.Count; i++)
                    audioSources.Add(Instance.gameObject.AddComponent("AudioSource") as AudioSource);
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
                    audioSources[_curTrack].clip = Instance.findClip(clips[_curTrack]);
                    audioSources[_curTrack].volume = volume;
                    audioSources[_curTrack].Play();
                    nextEventTime += audioSources[_curTrack].clip.length;
                }
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        // HELPER CLASSES
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        public class LevelLoopingCue
        {
            [HideInInspector]
            public string name;
            public LoopName loopName;
            public bool running = false;
            public int _curTrack;

            [Range(0.0f, 1.0f)]
            public float volume = 1.0f;

            private List<AudioSource> sources; 

            private AudioSource lightSource;
            public ClipName lightClip;
            private AudioSource mediumSource;
            public ClipName mediumClip;
            private AudioSource heavySource;
            public ClipName heavyClip;

            private bool mediumEnabled = false;
            private bool heavyEnabled = false;

            private double nextEventTime;

            public LevelLoopingCue(LoopName name, List<ClipName> clips)
            {
                this.name = AudioConstants.GetLoopName(name);
                lightClip = clips[0];
                mediumClip = clips[1];
                heavyClip = clips[2];
                sources = new List<AudioSource>();
            }
            public void Play()
            {
                lightSource = Instance.gameObject.AddComponent("AudioSource") as AudioSource;
                lightSource.clip = Instance.findClip(lightClip);
                lightSource.loop = true;
                lightSource.volume = 1.0f;
                sources.Add(lightSource);

                mediumSource = Instance.gameObject.AddComponent("AudioSource") as AudioSource;
                mediumSource.clip = Instance.findClip(mediumClip);
                mediumSource.volume = 0.0f;
                mediumSource.loop = true;
                sources.Add(mediumSource);

                heavySource = Instance.gameObject.AddComponent("AudioSource") as AudioSource;
                heavySource.clip = Instance.findClip(heavyClip);
                heavySource.volume = 0.0f;
                heavySource.loop = true;
                sources.Add(heavySource);

                foreach(var source in sources)
                    source.Play();

                running = true;
            }
            public void Stop()
            {
                running = false;
                foreach (var source in sources)
                    source.Stop();
            }

            public void enableLight()
            {
                if (mediumSource.volume > .99f)
                    Instance.StartCoroutine(this.fadeOut(mediumSource));
                if (heavySource.volume > .99f)
                    Instance.StartCoroutine(this.fadeOut(heavySource));
            }
            public void enableMedium()
            {
                if (mediumSource.volume < .01f)
                    Instance.StartCoroutine(this.fadeIn(mediumSource));
                if (heavySource.volume > .99f)
                    Instance.StartCoroutine(this.fadeOut(heavySource));
            }
            public void enableHeavy()
            {
                if(mediumSource.volume > .99f)
                    Instance.StartCoroutine(this.fadeOut(mediumSource));
                if(heavySource.volume < .01f)
                    Instance.StartCoroutine(this.fadeIn(heavySource));
            }

            public IEnumerator fadeIn(AudioSource s)
            {
                float fadeInTime = 2.0f;
                float fadingVolume = 0.0f;
                while (fadeInTime > 0)
                {
                    yield return new WaitForSeconds(.01f);
                    fadingVolume += .01f/fadeInTime;
                    s.volume = fadingVolume;
                    fadeInTime -= Time.deltaTime;
                }
                s.volume = 1.0f;
            }
            public IEnumerator fadeOut(AudioSource s)
            {
                float fadeOutTime = 2.0f;
                float fadingVolume = 1.0f;
                while (fadeOutTime > 0)
                {
                    yield return new WaitForSeconds(.01f);
                    fadingVolume -= .01f/fadeOutTime;
                    s.volume = fadingVolume;
                    fadeOutTime -= Time.deltaTime;
                }
                s.volume = 0.0f;
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
            public string Name;
            [HideInInspector]
            public CueName CueName;
            private readonly MultiCueType _cueType;
            [Range(0, 1.0f)]
            public float Volume;
            [SerializeField] 
            List<ClipName> clips; // List of all Clips
            ProportionValue<ClipName>[] _cueWeightProportions; // Percentage based Cue List
            private AudioSource source;

            public MultiCueType type
            {
                get
                {
                    return _cueType;
                }
            }

            // Random MultiCue
            public MultiCue(CueName name, List<ProportionValue<ClipName>> list)
            {
                this.CueName = name;
                this.Name = AudioConstants.GetCueName(name);
                clips = new List<ClipName>();
                foreach (var pv in list)
                    clips.Add(pv.Value);
                _cueType = MultiCueType.Random;
                Volume = 1.0f;
                _cueWeightProportions = list.ToArray();
            }
            // Parallel MultiCue
            public MultiCue(CueName name, List<ClipName> clipNameList)
            {
                this.CueName = name;
                this.Name = AudioConstants.GetCueName(name);
                clips = clipNameList;
                _cueType = MultiCueType.Parallel;
                Volume = 1.0f;
            }

            // plays this MultiCue at sourceObj's world position
            public bool play(GameObject sourceObj = null)
            {
                GameObject obj = sourceObj
                    ? sourceObj
                    : Instance.gameObject;

                switch (_cueType)
                {
                    case MultiCueType.Random:
                    {
                        ClipName c = _cueWeightProportions.ChooseByRandom();
                        return Instance.playClip(c, obj, Volume);
                    }
                    case MultiCueType.Parallel:
                        if (clips.Count == 0)
                            return false;
                        foreach (var clip in clips)
                            Instance.playClip(clip, obj, Volume);
                        return true;
                }
                return false;
            }
            public bool play(AudioSource s)
            {
                switch (_cueType)
                {
                    case MultiCueType.Random:
                        {
                            ClipName c = _cueWeightProportions.ChooseByRandom();
                            return Instance.playClip(c, s);
                        }
                    case MultiCueType.Parallel:
                        if (clips.Count == 0)
                            return false;
                        foreach (var clip in clips)
                            Instance.playClip(clip, s);
                        return true;
                }
                return false;
            }

            // sets the volume this multiCue will be played at
            public bool setVolume(float f)
            {
                if (f < 0 || f > 1)
                    return false;
                Volume = f;
                return true;
            }

            public float getCurrentTrackLength()
            {
                if (_cueType != MultiCueType.Parallel)
                    return 0.0f;
                float maxTime = 0.0f;
                foreach(var clip in clips)
                    if (Instance.findClip(clip).length > maxTime)
                        maxTime = Instance.findClip(clip).length;
                return maxTime;
            }
        }
    }
}