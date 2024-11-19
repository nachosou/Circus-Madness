using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Toggle fullScreenToggle;

    [SerializeField] private TMP_Dropdown resolutionsDropdown;
    private Resolution[] resolutions;

    private void Start()
    {
        if(Screen.fullScreen) 
        {
            fullScreenToggle.isOn = true;
        }
        else 
        { 
            fullScreenToggle.isOn = false;
        }

        CheckResolutions();
    }

    public void ActivateFullScreen(bool isFullScreenActive)
    {
        Screen.fullScreen = isFullScreenActive;
    }

    public void CheckResolutions()
    {
        resolutions = Screen.resolutions;
        resolutionsDropdown.ClearOptions();
        List<string> resolutionOptions = new List<string>();
        int actualResolution = 0;

        for (int i = 0; i < resolutions.Length; i++) 
        { 
            string option = resolutions[i].width + " x " + resolutions[i].height;
            resolutionOptions.Add(option);

            if(Screen.fullScreen && resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                actualResolution = i;
            }
        }

        resolutionsDropdown.AddOptions(resolutionOptions);
        resolutionsDropdown.value = actualResolution;
        resolutionsDropdown.RefreshShownValue();
    }

    public void ChangeResolutions(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
