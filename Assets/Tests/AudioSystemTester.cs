using AudioSystem;
using UnityEngine;

namespace Tests
{
    public class AudioSystemTester : MonoBehaviour
    {
        // [SerializeField] private AudioClip _clip;
        [SerializeField] private AudioEmitterSettings _settings;

        private void Update() {
            if (Input.GetKeyDown(KeyCode.F)) {
                TestSound();
            }
        }
        private void TestSound()
        {
            // AudioSystemManager.PlayClip(_clip);
            AudioSystemManager.PlayClip(_settings);
        }
    }
}