using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer = null;
    [SerializeField] private string _musicVolumeParameter = "MusicVolume";
    [SerializeField] private string _sfxVolumeParameter = "SFXVolume";
    [SerializeField] private Image _musicButtonImage = null;
    [SerializeField] private Sprite _musicEnabledImage = null;
    [SerializeField] private Sprite _musicDisabledImage = null;
    [SerializeField] private Image _sfxButtonImage = null;
    [SerializeField] private Sprite _sfxEnabledImage = null;
    [SerializeField] private Sprite _sfxDisabledImage = null;
    [SerializeField] private string _homeSceneName = "MainMenu";
    [SerializeField] private string _levelsSceneName = "LevelSelect";
    [SerializeField] private GameObject _quitPopup = null;

    public static bool MusicEnabled { get; private set; } = true;
    public static bool SFXEnabled { get; private set; } = true;

    private void Start()
    {
        UpdateMusicButton();
        UpdateSFXButton();
    }

    public void ToggleMusic()
    {
        MusicEnabled = !MusicEnabled;

        _audioMixer.SetFloat(_musicVolumeParameter, MusicEnabled ? -8 : -80);

        UpdateMusicButton();
    }

    public void ToggleSFX()
    {
        SFXEnabled = !SFXEnabled;

        _audioMixer.SetFloat(_sfxVolumeParameter, SFXEnabled ? 0 : -80);

        UpdateSFXButton();
    }

    void UpdateMusicButton()
    {
        if (_musicButtonImage)
            _musicButtonImage.sprite = MusicEnabled ? _musicEnabledImage : _musicDisabledImage;
    }

    void UpdateSFXButton()
    {
        if (_sfxButtonImage)
            _sfxButtonImage.sprite = SFXEnabled ? _sfxEnabledImage : _sfxDisabledImage;
    }

    public void MainMenu()
    {
        FindObjectOfType<SceneSwapper>().StartLoadScene(_homeSceneName);
    }

    public void LevelSelect()
    {
        FindObjectOfType<SceneSwapper>().StartLoadScene(_levelsSceneName);
    }

    public void StartQuit()
    {
        _quitPopup.SetActive(true);
    }

    public void CancelQuit()
    {
        _quitPopup.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
