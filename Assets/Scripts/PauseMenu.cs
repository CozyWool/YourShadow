using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public PlayerAim playerAim;

    public Slider slider;
    public Toggle toggle;
    public AudioMixerGroup mixer;
    public Marker marker;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !marker.inMiniGame)
        {
            if (GameIsPaused)
            {

                Resume();
            }
            else
            {
                Pause();
            }
        }

    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        playerAim.enabled = true;
        Time.timeScale = 1f;
        GameIsPaused = false;
        
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        playerAim.enabled = false;
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    public void Restart()
    {
        string RestartName = SceneManager.GetActiveScene().name;
        Debug.Log(RestartName);
        SceneTransition.SwitchToScene(RestartName);
        Time.timeScale = 1f;
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
