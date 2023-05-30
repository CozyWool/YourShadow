using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Slider slider;
    public Toggle toggle;
    public AudioMixerGroup mixer;
    public void Play()
    {
        SceneTransition.SwitchToScene("Lobby");
    }
    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    public void ToggleMusic()
    {
        if (toggle.isOn)
            mixer.audioMixer.SetFloat("MusicVolume", 0);
        else 
            mixer.audioMixer.SetFloat("MusicVolume", -80);
      
    }
    public void ChangeSound()
    { 
        mixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0, slider.value));
    }

}
