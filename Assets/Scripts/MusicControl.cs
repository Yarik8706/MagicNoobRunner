using System;
using System.Collections;
using UnityEngine;

public class MusicControl : MonoBehaviour
{
    [SerializeField] private AudioClip[] _musics;
    [SerializeField] private AudioSource _audioSource;
    
    private int _activeMusicIndex = 0;

    private void Start()
    {
        StartCoroutine(PlayMusicCoroutine());
    }
    
    private void RestartMusic()
    {
        StartCoroutine(PlayMusicCoroutine());
    }

    private IEnumerator PlayMusicCoroutine()
    {
        if (_activeMusicIndex >= _musics.Length)
        {
            _activeMusicIndex = 0;
        }
        var nextClip = _musics[_activeMusicIndex];
        _activeMusicIndex++;
        _audioSource.clip = nextClip;
        _audioSource.Play();
        yield return new WaitForSeconds(nextClip.length);
        _audioSource.Stop();

        RestartMusic();
    }
}