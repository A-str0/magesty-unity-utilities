using System.Collections;
using AudioSystem;
using UnityEditor.Timeline.Actions;
using UnityEngine;

namespace Tests
{
    public class AudioSystemTester : MonoBehaviour
    {
        [SerializeField] private AudioClip _clip;

        [SerializeField] private int _iterations = 12;
        [SerializeField] private float _delay = 1;

        private void Start()
        {
            StartCoroutine(Test());
        }

        private void TestSound()
        {
            AudioManager.Instance.PlayClip(_clip);
        }

        private IEnumerator Test()
        {
            for (int i = 0; i < _iterations; i++)
            {
                TestSound();
                yield return new WaitForSeconds(_delay);
            }
        }
    }
}