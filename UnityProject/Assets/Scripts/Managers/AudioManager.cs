#define TESTING
using System;
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
        [SerializeField]
        private List<LoopingCue> loops;

        private Dictionary<string, AudioClip> _oneShotList;
        private Dictionary<CueName, MultiCue> _cueDict;
        private Dictionary<LoopName, LoopingCue> _loopDict;

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

        public void UpdateManager()
        {
            DeleteClips();
            clips = new List<AudioClip>();
            cues = new List<MultiCue>();
            loops = new List<LoopingCue>();
            _oneShotList = new Dictionary<string, AudioClip>();
            _cueDict = new Dictionary<CueName, MultiCue>();
            _loopDict = new Dictionary<LoopName, LoopingCue>();

            foreach (var clip in Resources.LoadAll<AudioClip>("Arts/Music"))
                clips.Add(clip);

            foreach (AudioClip clip in clips)
                _oneShotList[clip.name] = clip;

            AudioConstants.CreateCustomCues();

            foreach (MultiCue cue in cues)
                _cueDict[cue.cueName] = cue;
        }
        public void DeleteClips()
        {
            //foreach (AudioSource s in _instance.gameObject.GetComponents<AudioSource>())
            //    DestroyImmediate(s);
            clips = new List<AudioClip>();
            cues = new List<MultiCue>();
            loops = new List<LoopingCue>();
            _oneShotList = new Dictionary<string, AudioClip>();
            _cueDict = new Dictionary<CueName, MultiCue>();
            _loopDict = new Dictionary<LoopName, LoopingCue>();
        }

        public bool playClip(ClipName name, GameObject sourceObject = null, float volume = 1.0f)
        {
            string clipName = AudioConstants.GetClipName(name);
            if (!_oneShotList.ContainsKey(clipName))
            {
                Debug.Log("AudioManager:playCue - Unable to locate AudioClip >>" + name + "<<\n");
                return false;
            }

            Vector3 playAt = sourceObject ? sourceObject.transform.position : this.transform.position;

            AudioSource.PlayClipAtPoint(findClip(name), playAt, volume);

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
        public bool playCue(CueName name, GameObject sourceObject = null, float volume = 1.0f)
        {
            if(_cueDict.ContainsKey(name))
                _cueDict[name].play();
            return true;
        }
        public bool createMultiCueRandom(CueName name, List<ClipName> clipList)
        {
            List<ProportionValue<ClipName>> list = new List<ProportionValue<ClipName>>();
            foreach( var clip in clipList)
                list.Add(ProportionValue.Create(1.0f / clipList.Count, clip));
            cues.Add(new MultiCue(name, list));
            return true;
        }
        public bool createMultiCueParallel(CueName name, List<ClipName> clipList)
        {
            cues.Add(new MultiCue(name, clipList));
            return true;
        }
        public bool createLoop(LoopName name, List<ClipName> clipList)
        {
            if (_loopDict.ContainsKey(name))
                return false;
            LoopingCue loopCue = new LoopingCue(name,clipList);
            _loopDict[name] = loopCue;
            loops.Add(loopCue);
            return true;
        }
        //////////////////////////////////////////////
        // LOOPING AUDIOCLIPS
        //////////////////////////////////////////////
        public bool playLoop(LoopName name, float volume = 1.0f)
        {
            if (!_loopDict.ContainsKey(name))
                return false;
            if (_loopDict[name].isPlaying)
                return false;
            _loopDict[name].volume = volume;
            _loopDict[name].start();
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
            _loopDict[name].stop();
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

        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        // HELPER CLASSES
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        [Serializable]
        public class LoopingCue
        {
            [HideInInspector]
            public string name;
            [HideInInspector]
            public LoopName loopName;
            [SerializeField]
            int _curTrack;
            [SerializeField]
            public bool isPlaying;
            [Range(0, 1.0f)]
            public float volume;
            [SerializeField]
            List<AudioClip> _trackList;

            [HideInInspector]
            private AudioSource source;

            public LoopingCue(LoopName name, List<ClipName> clipNames)
            {
                this.loopName = name;
                this.name = AudioConstants.GetLoopName(name);
                _trackList = new List<AudioClip>();
                _curTrack = 0;
                volume = 0.5f;
                isPlaying = false;
                foreach (ClipName clip in clipNames)
                    addTrack(clip);
            }

            public void start(bool loop = true)
            {
                if (_trackList.Count == 0)
                    return; // nothing to play
                if (source != null)
                    return; // already playing
                source = AudioManager.Instance.gameObject.AddComponent<AudioSource>();
                source.clip = _trackList[_curTrack];
                source.loop = false;
                source.volume = volume;
                playLoop();
                isPlaying = true;
            }
            public void playLoop()
            {
                source.clip = _trackList[_curTrack];
                source.Play();
                AudioManager.Instance.StartCoroutine(this.PlayAgain(this));
            }
            public void stop()
            {
                isPlaying = false;
                if (source == null)
                    return;
                AudioManager.Instance.DestroySource(source);
            }
            public void switchTrack()
            {
                if (_curTrack == _trackList.Count - 1)
                    _curTrack = 0; // go back to first track
                else
                    _curTrack++;
            }
            public bool addTrack(ClipName clip)
            {
                foreach (var track in _trackList)
                    if (track.name == AudioConstants.GetClipName(clip))
                        return false;
                _trackList.Add(AudioManager.Instance.findClip(clip));
                return true;
            }
            private IEnumerator PlayAgain(LoopingCue lewp)
            {
                yield return new WaitForSeconds(_trackList[_curTrack].length);
                if(lewp.source != null)
                    lewp.playLoop();
            }

        }
        public enum MultiCueType
        {
            //Sequential,
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
    public class ProportionValue<T>
    {
        public double Proportion { get; set; }
        public T Value { get; set; }
    }

    public static class ProportionValue
    {
        public static ProportionValue<T> Create<T>(double proportion, T value)
        {
            return new ProportionValue<T> { Proportion = proportion, Value = value };
        }

        static System.Random random = new System.Random();
        public static T ChooseByRandom<T>(
            this IEnumerable<ProportionValue<T>> collection)
        {
            var rnd = random.NextDouble();
            foreach (var item in collection)
            {
                if (rnd < item.Proportion)
                    return item.Value;
                rnd -= item.Proportion;
            }
            throw new InvalidOperationException(
                "The proportions in the collection do not add up to 1.");
        }
    }
}