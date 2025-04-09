using UnityEngine;

namespace AudioSystem
{
    public class AudioSystemManager 
    {
        public static void PlayClip(AudioClip clip)
        {
            GameObject obj = new GameObject();

            AudioEmitter emitter = obj.AddComponent<AudioEmitter>();

            emitter.Play(clip);
        }
    }
}