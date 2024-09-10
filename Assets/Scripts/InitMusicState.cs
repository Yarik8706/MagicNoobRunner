using System;
using UnityEngine;
using UnityEngine.Audio;
using YG;

namespace blocks
{
    public class InitMusicState : MonoBehaviour
    {
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private ChangeMusicStateUI[] _changeMusicStateUis;
        
        public static InitMusicState Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void Init()
        {
            if (YandexGame.savesData.mutedMusic == 1)
            {
                _audioMixer.SetFloat("MusicVolume", 0);
            }
            if (YandexGame.savesData.mutedEffects == 1)
            {
                _audioMixer.SetFloat("UIMusicVolume", 0);
            }

            foreach (var musicStateUi in _changeMusicStateUis)
            {
                musicStateUi.Init();
            }
        }
    }
}