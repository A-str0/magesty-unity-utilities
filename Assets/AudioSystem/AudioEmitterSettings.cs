using UnityEngine;
using UnityEngine.Audio;

namespace AudioSystem
{
    [CreateAssetMenu(fileName = "AudioEmitter Settings", menuName = "Audio/AudioEmitter Settings", order = 1)]
    public class AudioEmitterSettings : ScriptableObject
    {
        [SerializeField] private AudioClip _clip;
        [SerializeField] private AudioMixerGroup _mixerGroup;
        [SerializeField, Range(0f, 1f)] private float _volume = 1f;
        [SerializeField, Range(-3f, 3f)] private float _pitch = 1f;
        [SerializeField] private bool _loop;
        [SerializeField] private bool _playOnAwake;
        [SerializeField, Range(0f, 1f)] private float _spatialBlend = 0f; // 0 = 2D, 1 = 3D
        [SerializeField] private float _maxDistance = 500f;
        [SerializeField] private bool _destroyAfterPlayback = true;

        public AudioClip Clip => _clip;
        public AudioMixerGroup MixerGroup => _mixerGroup;
        public float Volume => _volume;
        public float Pitch => _pitch;
        public bool Loop => _loop;
        public bool PlayOnAwake => _playOnAwake;
        public float SpatialBlend => _spatialBlend;
        public float MaxDistance => _maxDistance;
        public bool DestroyAfterPlayback => _destroyAfterPlayback;
    }
}