using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class GenericAudioManager : MonoBehaviour
{
    public static GenericAudioManager Instance { get; private set; }
    private AudioSource _musicSource;
    private AudioSource _sfxSource;
    private Dictionary<string, AudioClip> _sfxDictionary;
    public Clip[] AudioClips;
    private Transform _playerPosition;
    [SerializeField] private float _sFXVolume = 0.5f;
    [SerializeField] private float _musicVolume = 0.2f;

    private void Awake() {
        if (Instance != null)
            Destroy(Instance);
        Instance = this;

        _musicSource = GetComponents<AudioSource>()[0];
        _musicSource.volume = _musicVolume;
        _sfxSource = GetComponents<AudioSource>()[1];
        _sfxSource.volume = _sFXVolume;
        InitDictionary();
    }

    private void InitDictionary() {
        _sfxDictionary = new Dictionary<string, AudioClip>();

        for(int i=0;i<AudioClips.Length;++i) {
            _sfxDictionary.Add(AudioClips[i].Name, AudioClips[i].SfxClip);
        }
    }

    public void PlayMusic(AudioClip music) {
        _musicSource.clip = music;
        _musicSource.Play();
    }

    public void PlaySfx(string clipName) {
        AudioClip clip;
        
        if(_sfxDictionary.TryGetValue(clipName, out clip)) {
            _sfxSource.PlayOneShot(clip, _sFXVolume);
        }
    }

    public void PlaySfx(string clipName, float volume) {
        AudioClip clip;
        
        if(_sfxDictionary.TryGetValue(clipName, out clip)) {
            _sfxSource.PlayOneShot(clip, volume);
        }
    }

    public IEnumerator FadeOutMusic()
    {
        WaitForSeconds sleepTime = new WaitForSeconds(0.01f);
        while (_musicSource.volume > 0)
        {
            _musicSource.volume -= Time.unscaledDeltaTime;
            yield return sleepTime;
        }
    }

    public IEnumerator FadeInMusic()
    {
        WaitForSeconds sleepTime = new WaitForSeconds(0.01f);
        while (_musicSource.volume < _musicVolume)
        {
            _musicSource.volume += Time.unscaledDeltaTime;
            yield return sleepTime;
        }
    }
}
