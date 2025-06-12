using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace MagestyUtilities.AudioSystem
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioEmitter : MonoBehaviour 
    {
        [SerializeField] private AudioEmitterSettings _settings;
        public UnityEvent PlaybackEnded;
        private AudioNamespace _parentThread;

        private AudioSource _audioSource;

        private void Awake() 
        {
            OnConfigure();
        }

        /// <summary>
        /// Method to configure the audio source
        /// </summary>
        private void OnConfigure()
        {
            if (_audioSource == null)
                _audioSource = GetComponent<AudioSource>();
            
            if (_settings != null)
                Configure(_settings);
        }

        /// <summary>
        /// Method for AudioSource configuration
        /// </summary>
        /// <param name="settings">ScriptableObject instance with configuration</param>
        public void Configure(AudioEmitterSettings settings)
        {
            _settings = settings;

            Debug.Log($"{settings}");
            Debug.Log($"{_audioSource.clip}; {settings.Clip}");

            _audioSource.clip = settings.Clip;
            _audioSource.outputAudioMixerGroup = settings.MixerGroup;
            _audioSource.volume = settings.Volume;
            _audioSource.pitch = settings.Pitch;
            _audioSource.loop = settings.Loop;
            _audioSource.playOnAwake = settings.PlayOnAwake;
            _audioSource.spatialBlend = settings.SpatialBlend;
            _audioSource.maxDistance = settings.MaxDistance;
        }

        public void SetParentThread(AudioNamespace thread)
        {
            _parentThread = thread;
        }

        /// <summary>
        /// Method for playing AudioSource given clip
        /// </summary>
        /// <param name="clip">AudioClip to play</param>
        public void Play(AudioClip clip)
        {
            _audioSource.clip = clip;
            Play();
        }

        /// <summary>
        /// Method for playing AudioSource
        /// </summary>
        public void Play()
        {
            StartCoroutine(WaitForEnd(_audioSource));
            _audioSource.Play();
        } 

        public void Stop()
        {
            _audioSource.Stop();
        }

        /// <summary>
        /// Coroutine for waiting for AudioSource plyback to end
        /// </summary>
        /// <param name="source">AudioSource to wait for</param>
        private IEnumerator WaitForEnd(AudioSource source)
        {
            float remainingTime = source.GetClipRemainingTime();
            WaitForSeconds waitForClipRemainingTime = new WaitForSeconds(remainingTime);
            yield return waitForClipRemainingTime;

            // Debug.Log($"({gameObject}) {source.clip} playback is ended. Was waiting for {remainingTime}");
            PlaybackEnded?.Invoke();
            _parentThread?.Release(this);
        }
    }
}