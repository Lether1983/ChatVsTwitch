using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SettingManager : MonoBehaviour
{
    [SerializeField]
    private MenuController controller;
    [SerializeField]
    private Toggle fullscreenToggle;
    [SerializeField]
    private Dropdown resolutionDropdown;
    [SerializeField]
    private Dropdown textureQualityDropdown;
    [SerializeField]
    private Dropdown antialiasingDropdown;
    [SerializeField]
    private Dropdown vSyncDropdown;
    [SerializeField]
    private Slider musicVolumeSlider;
    [SerializeField]
    private Button applyButton;

    private Resolution[] resolutions;
    private GameSettings gameSettings;

    private void OnEnable()
    {
        gameSettings = new GameSettings();

        fullscreenToggle.onValueChanged.AddListener(delegate { OnFullscreenToggleChange(); });
        resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        textureQualityDropdown.onValueChanged.AddListener(delegate { OnTextureQualtiyChange(); });
        antialiasingDropdown.onValueChanged.AddListener(delegate { OnAntialiasingChange(); });
        vSyncDropdown.onValueChanged.AddListener(delegate { OnVSyncChange(); });
        musicVolumeSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChange(); });
        applyButton.onClick.AddListener(delegate { OnApplyButtonClick(); });

        resolutions = Screen.resolutions;
        foreach (Resolution resolution in resolutions)
        {
            resolutionDropdown.options.Add(new Dropdown.OptionData(resolution.ToString()));
        }
        LoadSettings();
    }

    public void OnFullscreenToggleChange()
    {
        gameSettings.fullscreen = Screen.fullScreen = fullscreenToggle.isOn;
    }

    public void OnResolutionChange()
    {
        Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height, Screen.fullScreen);
        gameSettings.resolutionIndex = resolutionDropdown.value;
    }

    public void OnTextureQualtiyChange()
    {
        QualitySettings.masterTextureLimit = gameSettings.textureQuality = textureQualityDropdown.value;
    }

    public void OnAntialiasingChange()
    {
        QualitySettings.antiAliasing = gameSettings.antialiasing = (int)Mathf.Pow(2,antialiasingDropdown.value);
    }

    public void OnVSyncChange()
    {
        QualitySettings.vSyncCount = gameSettings.vSync = vSyncDropdown.value;
    }

    public void OnMusicVolumeChange()
    {
        //TODO: MusicSource für Background Music hinzufügen.
        gameSettings.musicVolume = musicVolumeSlider.value;
    }

    public void OnApplyButtonClick()
    {
        SaveSettings();
        controller.ActiveMenuPanel();
    }

    public void SaveSettings()
    {
        string jsonData = JsonUtility.ToJson(gameSettings,true);
        File.WriteAllText(Application.persistentDataPath + "/gamesettings.json", jsonData); 
    }

    public void LoadSettings() 
    {
        gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));
        musicVolumeSlider.value = gameSettings.musicVolume;
        antialiasingDropdown.value = gameSettings.antialiasing;
        vSyncDropdown.value = gameSettings.vSync;
        textureQualityDropdown.value = gameSettings.textureQuality;
        resolutionDropdown.value = gameSettings.resolutionIndex;
        fullscreenToggle.isOn = gameSettings.fullscreen;
        Screen.fullScreen = gameSettings.fullscreen;
    }
}

