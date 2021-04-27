using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Music : MonoBehaviour
{
    // These are all slider and toggles on the settings pannel.
    public Slider musicSlider;
    public Slider pitchSlider;
 
    public AudioMixer musicAudio;
    public AudioMixer masterAudio;

    private void Start()
    {
        LoadPlayerPrefs2();
    }

    // Sets up Quality drop down.    
    public void ChangeVolume(float volume)
    {
        musicAudio.SetFloat("MusicVol", volume);
    }

    public void ChangePitch(float volume)
    {
        musicAudio.SetFloat("PitchShift", volume);
    }

    // On disable run this
    private void OnDisable()
    {
        SavePlayerPrefs2();
    }

    public void SavePlayerPrefs2()
    {
        // Gets volume, sets volume and saves volume.
        float volume;
        if (masterAudio.GetFloat("MusicVol", out volume))       // "MusicVol" is the "Exposed Parameters" in the Audio Mixer
        {
            PlayerPrefs.SetFloat("MusicVol", volume);
        }

        // Gets pitch, sets pitch and saves pitch.
        float Pitch;
        if (masterAudio.GetFloat("PitchShift", out Pitch))       // "volumeDistortion" is the "Exposed Parameters" in the Audio Mixer
        {
            PlayerPrefs.SetFloat("PitchShift", Pitch);
        }
    }

    // On start run volume.
    public void LoadPlayerPrefs2()
    {
        // Gets volume sets volume and saves volume.
        if (PlayerPrefs.HasKey("MusicVol"))
        {
            float musicVol = PlayerPrefs.GetFloat("MusicVol");
            masterAudio.SetFloat("MusicVol", musicVol);
            musicSlider.value = musicVol;
        }

        // Gets volume sets pitch and saves pitch.
        if (PlayerPrefs.HasKey("PitchShift"))
        {
            float pitchShift = PlayerPrefs.GetFloat("PitchShift");
            masterAudio.SetFloat("PitchShift", pitchShift);
            pitchSlider.value = pitchShift;
        }
    }
}