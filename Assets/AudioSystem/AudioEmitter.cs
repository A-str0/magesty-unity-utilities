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
            // _audioSource.Play();
            // StartCoroutine(WaitForEnd(_audioSource));
            StartCoroutine(Play(_audioSource));
        } 

        private IEnumerator Play(AudioSource source)
        {
            Debug.Log("Started waiting");
            float remainingTime = source.GetClipRemainingTime();

            _audioSource.Play();
            yield return new WaitForSeconds(remainingTime);

            Debug.Log($"({gameObject}) {source.clip} playback is ended. Was waiting for {remainingTime}");
            PlaybackEnded?.Invoke();

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