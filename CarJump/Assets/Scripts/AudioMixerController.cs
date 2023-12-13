using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixerController : MonoBehaviour
{
    public AudioMixer audioMixer;
    
    public Slider music;
    public Slider sfx;

    private const string MusicVolumeKey = "MusicVolume";
    private const string SFXVolumeKey = "SFXVolume";

    private bool slidersEnabled = true;

    private void Start()
    {
        LoadAudioSettings();
    }

    public void MusicChange()
    {
        if (slidersEnabled)
        {
            
            audioMixer.SetFloat("MenuMusic", music.value);
            SaveAudioSettings();
        }
    }
    
    public void SFXChange()
    {
        if (slidersEnabled)
        {
            audioMixer.SetFloat("SFXSound", sfx.value);
            SaveAudioSettings();
        }
    }

    private void LoadAudioSettings()
    {
        if (PlayerPrefs.HasKey(MusicVolumeKey))
        {
            float musicVolume = PlayerPrefs.GetFloat(MusicVolumeKey);
            music.value = musicVolume;
            audioMixer.SetFloat("MenuMusic", musicVolume);
        }

        if (PlayerPrefs.HasKey(SFXVolumeKey))
        {
            float sfxVolume = PlayerPrefs.GetFloat(SFXVolumeKey);
            sfx.value = sfxVolume;
            audioMixer.SetFloat("SFXSound", sfxVolume);
        }

        EnableSliders(true);
    }

    private void SaveAudioSettings()
    {
        PlayerPrefs.SetFloat(MusicVolumeKey, music.value);
        PlayerPrefs.SetFloat(SFXVolumeKey, sfx.value);
    }

    private void EnableSliders(bool enable)
    {
        slidersEnabled = enable;
        music.interactable = enable;
        sfx.interactable = enable;
    }

    public void StartGame()
    {
        EnableSliders(false);
        // Oyun başlatma kodlarını buraya ekleyin
    }
}
