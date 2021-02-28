using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BumperCarGamePrototype.Concretes.Managers
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioClip _winClip;
        [SerializeField] private AudioClip _loseClip;
        private AudioSource _audioSource;
        private float _playSoundTime = .5f;
        private float _currentTime;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        private void Update()
        {
            _currentTime += Time.deltaTime;
            if (_currentTime >= _playSoundTime) _currentTime = _playSoundTime;
        }
        public void PlaySound(string clip)
        {
            if (_currentTime == _playSoundTime)
            {
                switch (clip)
                {
                    case "Win":
                        _audioSource.PlayOneShot(_winClip);
                        break;
                    case "Lose":
                        _audioSource.PlayOneShot(_loseClip);
                        break;
                }
                _currentTime = 0.0f;
            }
        }
    }
}