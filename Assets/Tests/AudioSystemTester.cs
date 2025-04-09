using AudioSystem;
using UnityEditor.Timeline.Actions;
using UnityEngine;

namespace Tests
{
    public class AudioSystemTester : MonoBehaviour
    {
        [SerializeField] private AudioClip _clip;

        private void Start() 
        {
            TestSound();
        }

        private void TestSound()
        {
            AudioSystemManager.PlayClip(_clip);
        }
    }
}