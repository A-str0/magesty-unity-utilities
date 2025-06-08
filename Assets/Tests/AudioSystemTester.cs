using System.Collections;
using UnityEngine;
using AudioSystem;

namespace Tests
{
    public class AudioSystemTester : MonoBehaviour
    {
        [SerializeField] private AudioEmitterSettings _settings;
        [SerializeField] private int _iterations = 3;
        [SerializeField] private float _delay = 1;

        private void Start()
        {
            if (_settings == null)
            {
                Debug.LogError("AudioSystemTester: _settings is null! Please assign in Inspector.", this);
                return;
            }
            StartCoroutine(Test());
        }
        
        private void TestSound()
        {
            if (_settings == null)
            {
                Debug.LogError("AudioSystemTester: Cannot play sound, _settings is null!", this);
                return;
            }
            Debug.Log($"AudioSystemTester: Playing clip via _settings on thread '{_settings}'", this);
            AudioManager.Instance.PlayClip(_settings);
        }

        private IEnumerator Test()
        {
            for (int i = 0; i < _iterations; i++)
            {
                yield return new WaitForSeconds(_delay);
                Debug.Log($"AudioSystemTester: Iteration {i + 1} of {_iterations}");
                TestSound();
            }
        }
    }
}