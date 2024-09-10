using System;
using UnityEngine;
using UnityEngine.Audio;

public enum SoundType
{
    UIMusic,
    Music
}

public class SoundManager : MonoBehaviour
{
    public AudioClip buttonAudio;
    [SerializeField] private AudioSource _audioSource;

    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void PlayAudioSound(AudioClip audioClip)
    {
        _audioSource.clip = audioClip;
        _audioSource.Play();
    }
}