using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Optiuni : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;
    public Slider volumeSlider;

    private Resolution[] resolutions;

    private void Start()
    {
        // Get available screen resolutions
        resolutions = Screen.resolutions;

        // Clear existing options in dropdown
        resolutionDropdown.ClearOptions();

        // Populate dropdown with available resolutions
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            // Check if current resolution matches
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // Set initial toggle value
        fullscreenToggle.isOn = Screen.fullScreen;

        // Set initial volume slider value
        volumeSlider.value = PlayerPrefs.GetFloat("volume", 1f);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetVolume(float volume)
    {
        // Save volume setting to player prefs
        PlayerPrefs.SetFloat("volume", volume);

        // Set audio listener volume
        AudioListener.volume = volume;
    }
}
