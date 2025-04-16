using UnityEngine;

namespace AudioSystem
{
    public class AudioSystemManager 
    {
        public static void PlayClip(AudioEmitterSettings settings, Vector3 position = default)
        {
            GameObject obj = new GameObject();
            obj.name = $"{settings.Clip.name} clip emitter";
            obj.transform.position = position;

            AudioEmitter emitter = obj.AddComponent<AudioEmitter>();
            emitter.Configure(settings);
            emitter.Play();
        }

        /// <summary>
        /// Overload for simple AudioClip play with default settings
        /// </summary>
        /// <param name="clip">Clip to play</param>
        /// <param name="position">AudioSource position in 3D</param>
        public static void PlayClip(AudioClip clip, Vector3 position = default)
        {
            AudioEmitterSettings defaultSettings = ScriptableObject.CreateInstance<AudioEmitterSettings>();
            defaultSettings.GetType().GetField("_clip", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(defaultSettings, clip);
            PlayClip(defaultSettings, position);
        }
    }
}