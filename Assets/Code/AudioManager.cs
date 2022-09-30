using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private PlayerDataManager _playerDataManager;
    [SerializeField] private List<AudioClip> _backgroundSounds;
    [SerializeField] private AudioSource _backgroundMusicSource;
    [SerializeField] private AudioSource _SFXAndVFXSource;
    [SerializeField] private AudioSource _VFXSource;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private ToggleButton _turnOnOffSound;
    [SerializeField] private AudioClip _clickSound;
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private Slider _soundVolumeSlider;

    private bool _isAudioOn; //Is audio playing?
    private bool _gotMusicToPlay; //Are audio clips loaded?
    private bool _isPlayingVFXLoop; //loop vfx sounds?
    private Coroutine _playAudioCoroutine; //Reference to audio coroutine
    private void Awake()
    {
        _gameManager.OnPlayerDataInitialized += DataInitialized;
        _turnOnOffSound.OnToggleChanged += ChangeSoundPlayStatus;
        _musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        _soundVolumeSlider.onValueChanged.AddListener(SetSoundVolume);
    }

    private void SetSoundVolume(float value)  //VFX sound volume
    {
        _playerDataManager.PlayerData.SfxVolume = value;
        value = Mathf.Log10(value) * 20;
        _audioMixer.SetFloat("SFX", value);        
    }

    private void SetMusicVolume(float value) //Background sound volume
    {
        _playerDataManager.PlayerData.AudioVolume = value;
        value = Mathf.Log10(value) * 20;
        _audioMixer.SetFloat("Background", value);
    }

    private void ChangeSoundPlayStatus(bool status) //Turn On/Off audio
    {
        _isAudioOn = status;
        _playerDataManager.PlayerData.IsAudioOn = status;
        TryPlayMusic(status);
    }

    private void DataInitialized(PlayerData playerData) 
    {
        _isAudioOn = playerData.IsAudioOn;
        _turnOnOffSound.Set(_isAudioOn);
        TryPlayMusic(_isAudioOn);
        _musicVolumeSlider.value = playerData.AudioVolume;
        _soundVolumeSlider.value = playerData.SfxVolume;
        _audioMixer.SetFloat("Background", Mathf.Log10(playerData.AudioVolume) * 20);
        _audioMixer.SetFloat("SFX", Mathf.Log10(playerData.SfxVolume) * 20);

    }

    public void PlayButtonClick() //On button click
    {
        if(_isAudioOn)
            _SFXAndVFXSource.PlayOneShot(_clickSound);
    }

    public void PlayOnceSFX(AudioClip clip) //SFX - UI sounds
    {
        if (_isAudioOn && clip != null)
            _SFXAndVFXSource.PlayOneShot(clip);
    }

    public void PlayOnceVFX(AudioClip clip) //VFX - in game sounds
    {
        if (_isAudioOn && clip != null)
            _VFXSource.PlayOneShot(clip);
    }

    public void TryPlayMusic(bool status) 
    {
        if (status)
            _playAudioCoroutine = StartCoroutine(PlayBackgroundMusic());
        else
        {
            if (_playAudioCoroutine != null)
                StopAllCoroutines();
            _backgroundMusicSource.Stop();
        }
    }

    public void PlayVFXSound(AudioClip clip, bool status)
    {
        //Turn on and off vfx loop sound
        if (status && !_isPlayingVFXLoop) 
        {
            _isPlayingVFXLoop = true;
            _SFXAndVFXSource.clip = clip;
            _SFXAndVFXSource.Play();
            _SFXAndVFXSource.loop = true;
        }
        else
        {
            _isPlayingVFXLoop = false;
            _SFXAndVFXSource.clip = null;
            _SFXAndVFXSource.Stop();
            _SFXAndVFXSource.loop = false;
        }
    }

    private IEnumerator PlayBackgroundMusic()
    {
        //Background music is loaded by adressables,
        //So if _backgroundSounds is empty check again after 0.5f

        while (_backgroundSounds.Count == 0) 
            yield return new WaitForSeconds(0.5f);
        int index = UnityEngine.Random.Range(0, _backgroundSounds.Count);
        _backgroundMusicSource.clip = _backgroundSounds[index];
        _backgroundMusicSource.Play();
        yield return new WaitForSeconds(_backgroundSounds[index].length);
        _playAudioCoroutine = StartCoroutine(PlayBackgroundMusic());
    }

    public void UpdateList(AudioClip clip)
    {
        if(!_gotMusicToPlay)
            _gotMusicToPlay = true;
        if (clip == null)
            return;
        if(!_backgroundSounds.Contains(clip))
            _backgroundSounds.Add(clip);
    }
}
