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
        [SerializeField]
        private List<LoopingCue> loops;
        private Dictionary<string, AudioClip> _oneShotList;
        private Dictionary<string, MultiCue> _cueDict;
        private Dictionary<string, LoopingCue> _loopDict;

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
            clips = new List<AudioClip>();
            cues = new List<MultiCue>();
            _oneShotList = new Dictionary<string, AudioClip>();
            _cueDict = new Dictionary<string, MultiCue>();
            _loopDict = new Dictionary<string, LoopingCue>();

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
                        //Debug.Log("Created Parallel Cue -> " + sd.Name + ": " + cuenames);
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
                        //Debug.Log("Created Random Cue -> " + sd.Name + ": " + cuenames);
                        createMultiCueRandom(sd.Name, clipList);
                    }
                }
                else if (d.Name.Equals("Sequential"))
                {
                    foreach (var sd in d.GetDirectories())
                    {
                        List<AudioClip> clipList = new List<AudioClip>();
                        string cuenames = "";
                        int index = 0;
                        foreach (var clip in Resources.LoadAll<AudioClip>("Arts/Music/Cues/Sequential/" + sd.Name))
                        {
                            clipList.Add(clip);
                            cuenames += "[" + index + "](" + clip.name + ") ";
                            index++;
                        }
                        //Debug.Log("Created Seq Cue -> " + sd.Name + ": " + cuenames);
                        createMultiCueSequential(sd.Name, clipList);
                    }
                }
            }

            foreach (var clip in clips)
                _oneShotList[clip.name] = clip;
            foreach (var cue in cues)
                _cueDict[cue.name] = cue;

        }
        public void DeleteClips()
        {
            clips = new List<AudioClip>();
            cues = new List<MultiCue>();
            loops = new List<LoopingCue>();
            _oneShotList = new Dictionary<string, AudioClip>();
            _cueDict = new Dictionary<string, MultiCue>();
            _loopDict = new Dictionary<string, LoopingCue>();
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
        public bool playClip(ClipName name, GameObject sourceObject = null, float volume = 1.0f)
        {
            string clipName = AudioConstants.GetClipName(name);
            if (!_oneShotList.ContainsKey(clipName))
            {
                Debug.Log("AudioManager:playCue - Unable to locate AudioClip >>" + name + "<<\n");
                return false;
            }

            Vector3 playAt = sourceObject ? sourceObject.transform.position : this.transform.position;

            AudioSource.PlayClipAtPoint(findClip(clipName), playAt, volume);

            return true;
        }
        public bool playClipDelayed(string name, float delayTime, GameObject sourceObject = null, float volume = 1.0f)
        {
            if (!_oneShotList.ContainsKey(name))
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
        public bool playCue(string name, GameObject sourceObject = null, float volume = 1.0f)
        {
            if (!_cueDict.ContainsKey(name))
                return false;
            _cueDict[name].play();
            return true;
        }

        /*
        public bool createMultiCueWeighted(string name, List<KeyValuePair<string,int>> cueWeightList)
        {
            if (_cueDict.ContainsKey(name))
                return false;

            cues.Add(new MultiCue(name, cueWeightList));
            return true;
        }*/
        public bool createMultiCueRandom(string name, List<AudioClip> clipList)
        {
            if (_cueDict.ContainsKey(name))
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
        public bool createMultiCueParallel(string name, List<AudioClip> clipList)
        {
            if (_cueDict.ContainsKey(name))
                return false;
            cues.Add(new MultiCue(name, clipList));
            return true;
        }
        public bool createMultiCueSequential(string name, List<AudioClip> clipList)
        {
            if (_cueDict.ContainsKey(name))
                return false;

            Dictionary<int,string> seqList = new Dictionary<int,string>();

            for(int i = 0; i < clipList.Count; i++)
                seqList[i] = clipList[i].name;

            cues.Add(new MultiCue(name,seqList));
            return true;
        }
        public bool createLoop(LoopingCue cue)
        {
            if (_loopDict.ContainsKey(cue.name))
                return false;
            _loopDict[cue.name] = cue;
            loops.Add(cue);
            return true;
        }
        //////////////////////////////////////////////
        // LOOPING AUDIOCLIPS
        //////////////////////////////////////////////
        public bool playLoop(string name, float volume = 1.0f)
        {
            if (!_loopDict.ContainsKey(name))
                return false;
            if (_loopDict[name].isPlaying)
                return false;
            _loopDict[name].volume = volume;
            _loopDict[name].start();
            return true;
        }
        public bool swapLoopTrack(string name)
        {
            if (!_loopDict.ContainsKey(name))
                return false;
            _loopDict[name].switchTrack();
            return true;
        }
        public bool stopLoop(string name)
        {
            if (!_loopDict.ContainsKey(name))
                return false;
            _loopDict[name].stop();
            return true;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        // HELPER FUNCTIONS
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        public AudioClip findClip(string name)
        {
            if (_oneShotList.ContainsKey(name))
                return _oneShotList[name];
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
            [SerializeField]
            public string name;
            [SerializeField]
            int _curTrack;
            [SerializeField]
            public bool isPlaying;
            [Range(0, 1.0f)]
            public float volume;
            [SerializeField]
            List<AudioClip> _trackList;
            public AudioSource source;

            public LoopingCue(string name)
            {
                this.name = name;
                _trackList = new List<AudioClip>();
                _curTrack = 0;
                volume = 0.75f;
                isPlaying = false;
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
            public bool addTrack(AudioClip clip)
            {
                foreach (var track in _trackList)
                    if (track.name == clip.name)
                        return false;
                _trackList.Add(clip);
                return true;
            }
            private IEnumerator PlayAgain(LoopingCue lewp)
            {
                yield return new WaitForSeconds(_trackList[_curTrack].length);
                if(lewp.source != null)
                    lewp.playLoop();
            }

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
            List<AudioClip> clips; // List of all Clips
            List<KeyValuePair<string, int>> cueWeightList; // Weighted Cue List
            int totalWeight;
            Dictionary<int, string> seqList; // Sequential Cue List
            int totalSequences;
            int currentSeq;

            public MultiCueType type
            {
                get
                {
                    return CueType;
                }
            }

            public MultiCue(string name, List<KeyValuePair<string, int>> inputList)
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
                foreach (var entry in seqList)
                    clips.Add(AudioManager.Instance.findClip(entry.Value));

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
                    if (clips.Count == 0)
                        return false;
                    float totalTime = 0.0f;
                    int curSeq = 0;

                    while (curSeq != totalSequences)
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
                if (f < 0 || f > 1)
                    return false;
                volume = f;
                return true;
            }
        }
    }
}