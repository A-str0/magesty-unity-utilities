using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AudioSystem
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [System.Serializable]
        private struct AudioThreadConfig
        {
            public string threadName;
            public int maxPoolCapacity;
            public int defaultPoolCapacity;
        }

        [SerializeField]
        private List<AudioThreadConfig> threadConfigs = new List<AudioThreadConfig>
        {
            new AudioThreadConfig { threadName = "default", maxPoolCapacity = 10, defaultPoolCapacity = 5 }
        };

        private Dictionary<string, AudioNamespace> threads = new Dictionary<string, AudioNamespace>();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                InitializeThreads();
            }
            else
            {
                Debug.LogWarning("AudioManager instance already exists, destroying duplicate");
                Destroy(gameObject);
            }
        }

        private void InitializeThreads()
        {
            foreach (var config in threadConfigs)
            {
                if (string.IsNullOrEmpty(config.threadName))
                {
                    Debug.LogError("AudioManager: Thread name cannot be empty in config!");
                    continue;
                }

                if (threads.ContainsKey(config.threadName))
                {
                    Debug.LogWarning($"AudioManager: Duplicate thread name '{config.threadName}' skipped");
                    continue;
                }

                threads[config.threadName] = new AudioNamespace(
                    maxPoolCapacity: config.maxPoolCapacity,
                    defaultPoolCapacity: config.defaultPoolCapacity
                );
            }

            if (!threads.ContainsKey("default"))
            {
                Debug.LogWarning("No 'default' thread configured, creating one automatically");
                AddThread("default", 10, 5);
            }
        }

        public void AddThread(string threadName, int maxPoolCapacity, int defaultPoolCapacity)
        {
            if (string.IsNullOrEmpty(threadName))
            {
                Debug.LogError("AudioManager: Cannot add thread with empty name!");
                return;
            }

            if (threads.ContainsKey(threadName))
            {
                Debug.LogWarning($"AudioManager: Thread '{threadName}' already exists, skipping addition");
                return;
            }

            if (maxPoolCapacity < 1 || defaultPoolCapacity < 1)
            {
                Debug.LogError($"AudioManager: Invalid pool capacities (max: {maxPoolCapacity}, default: {defaultPoolCapacity}) for thread '{threadName}'");
                return;
            }

            // Create and add the new thread
            threads[threadName] = new AudioNamespace(maxPoolCapacity, defaultPoolCapacity);

            // Update the serialized config for Inspector visibility
            threadConfigs.Add(new AudioThreadConfig
            {
                threadName = threadName,
                maxPoolCapacity = maxPoolCapacity,
                defaultPoolCapacity = defaultPoolCapacity
            });

            // Pro tip: Mark object as dirty to ensure Inspector updates persist in Editor
            #if UNITY_EDITOR
            EditorUtility.SetDirty(this);
            #endif

            Debug.Log($"AudioManager: Thread '{threadName}' added with maxPool: {maxPoolCapacity}, defaultPool: {defaultPoolCapacity}");
        }

        public void PlayClip(AudioClip clip, string threadName = "default")
        {
            if (clip == null)
            {
                Debug.LogError("AudioManager: Cannot play null AudioClip!");
                return;
            }
            
            if (threads.TryGetValue(threadName, out AudioNamespace thread))
            {
                AudioEmitter emitter = thread.Get();
                emitter.Play(clip);
            }
            else
            {
                Debug.LogWarning($"AudioManager: Thread '{threadName}' not found, using default");
                threads["default"].Get().Play(clip);
            }
        }
    }
}