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

            threads[threadName] = new AudioNamespace(maxPoolCapacity, defaultPoolCapacity);

            threadConfigs.Add(new AudioThreadConfig
            {
                threadName = threadName,
                maxPoolCapacity = maxPoolCapacity,
                defaultPoolCapacity = defaultPoolCapacity
            });

            // ensuring inspector updates in Editor
            #if UNITY_EDITOR
            EditorUtility.SetDirty(this);
            #endif

            Debug.Log($"AudioManager: Thread '{threadName}' added with maxPool: {maxPoolCapacity}, defaultPool: {defaultPoolCapacity}");
        }

        private AudioEmitter GetEmitter(string threadName)
        {
            if (threads.TryGetValue(threadName, out AudioNamespace thread))
            {
                // Debug.Log($"Getting AudioEmitter from {threadName} thread");
                return thread.Get();
            }
            else
            {
                Debug.LogWarning($"AudioManager: Thread '{threadName}' not found, using default");
                return threads["default"].Get();
            }
        }

        /// <summary>
        /// Method for creating AudioEmitter instance with given settings
        /// </summary>
        /// <param name="settings">AudioEmitterSetiings to play sound with</param>
        /// <param name="position">AudioSource position in world</param>
        /// <param name="parent">Parent for AudioEmitter</param>
        public void PlayClip(AudioEmitterSettings settings, Vector3 position = default, Transform parent = default, string threadName = "default")
        {
            AudioEmitter emitter = GetEmitter(threadName);

            GameObject obj = emitter.gameObject;
            obj.transform.position = position;
            obj.transform.SetParent(parent);

            emitter.Configure(settings);
            emitter.Play();
        }

        /// <summary>
        /// Overload for simple AudioClip play with default AudioSource settings
        /// </summary>
        /// <param name="clip">Clip to play</param>
        /// <param name="position">AudioSource position in world</param>
        /// <param name="parent">Parent for AudioEmitter</param>
        public void PlayClip(AudioClip clip, Vector3 position = default, Transform parent = default, string threadName = "default")
        {
            AudioEmitterSettings defaultSettings = ScriptableObject.CreateInstance<AudioEmitterSettings>();
            defaultSettings.GetType().GetField("_clip", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(defaultSettings, clip);
            PlayClip(defaultSettings, position, parent, threadName);
        }
    }
}