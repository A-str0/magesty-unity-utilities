using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace AudioSystem
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioEmitter : MonoBehaviour 
    {
        [SerializeField] private bool _playOnAwake;

        public UnityEvent PlaybackEnded;
        private AudioNamespace _parentThread;

        private AudioSource _audioSource;

        private void Awake() 
        {
            if (_playOnAwake) Play();

            OnConfigure();
        }

        private void OnConfigure()
        {
            if (_audioSource == null)
                _audioSource = GetComponent<AudioSource>();
            
            _audioSource.playOnAwake = false;
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

        public void Stop()
        {
            _audioSource.Stop();
        }

        /// <summary>
        /// Method for playing AudioSource
        /// </summary>
        public void Play()
        {
            StartCoroutine(PlayCoroutine(_audioSource));
        }

        private IEnumerator PlayCoroutine(AudioSource source)
        {
            Debug.Log("Started waiting");
            float remainingTime = source.GetClipRemainingTime();

            _audioSource.Play();
            yield return new WaitForSeconds(remainingTime);

            Debug.Log($"({gameObject}) {source.clip} playback is ended. Was waiting for {remainingTime}");
            PlaybackEnded?.Invoke();

            _parentThread?.Release(this);
        }

        /// <summary>
        /// Coroutine for waiting for AudioSource plyback to end
        /// </summary>
        /// <param name="source">AudioSource to wait for</param>
        private IEnumerator WaitForEnd(AudioSource source)
        {
            Debug.Log("Started waiting");
            float remainingTime = source.GetClipRemainingTime();
            WaitForSeconds waitForClipRemainingTime = new WaitForSeconds(remainingTime);
            yield return waitForClipRemainingTime;

            Debug.Log($"({gameObject}) {source.clip} playback is ended. Was waiting for {remainingTime}");
            PlaybackEnded?.Invoke();
        }
    }
}