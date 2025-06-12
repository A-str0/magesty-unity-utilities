using System;
using UnityEngine;
using UnityEngine.Pool;

namespace MagestyUtilities.AudioSystem
{
    [Serializable]
    public class AudioNamespace
    {
        private ObjectPool<AudioEmitter> _objectsPool;

        public AudioNamespace(int maxPoolCapacity, int defaultPoolCapacity)
        {
            _objectsPool = new ObjectPool<AudioEmitter>(
                createFunc: CreateEmitter,
                actionOnGet: OnGetEmitter,
                actionOnRelease: OnReleaseEmitter,
                actionOnDestroy: OnDestroyEmitter,
                defaultCapacity: defaultPoolCapacity,
                maxSize: maxPoolCapacity
            );
        }

        public AudioEmitter Get()
        {
            AudioEmitter emitter = _objectsPool.Get();
            emitter.SetParentThread(this);
            return emitter;
        }

        public void Release(AudioEmitter emitter)
        {
            _objectsPool.Release(emitter);
        }

        private AudioEmitter CreateEmitter()
        {
            GameObject obj = new GameObject("AudioEmitter");
            AudioEmitter emitter = obj.AddComponent<AudioEmitter>();
            return emitter;
        }

        private void OnGetEmitter(AudioEmitter obj)
        {
            obj.gameObject.SetActive(true);
        }

        private void OnReleaseEmitter(AudioEmitter obj)
        {
            obj.gameObject.SetActive(false);
        }

        private void OnDestroyEmitter(AudioEmitter obj)
        {
            Debug.Log($"{this} Object pool is overloaded");
        }
    }
}