using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour {

    public Toggle fullscreenToggle;
    public Dropdown resolutionDropdown;
    public Dropdown textureQualityDropdown;
    public Dropdown antialiasingDropdown;
    public Dropdown vSyncDropdown;
    public Slider musicVolumeSlider;
    public Button applyButton;
    public Button exitButton;
    public Button saveButton;
    public Button loadButton;

    public AudioSource musicSource;
    public Resolution[] resolutions;
    public GameSettings gameSettings;

   
    private void OnEnable()
    {
        gameSettings = new GameSettings();

        fullscreenToggle.onValueChanged.AddListener(delegate { OnFullscreenToggle(); });
        resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        textureQualityDropdown.onValueChanged.AddListener(delegate { OnTextureQualityChange(); });
        antialiasingDropdown.onValueChanged.AddListener(delegate { OnAntialiasingChange(); });
        vSyncDropdown.onValueChanged.AddListener(delegate { OnVSyncChange(); });
        musicVolumeSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChange(); });
        applyButton.onClick.AddListener(delegate { OnApplyButtonClick(); });
        exitButton.onClick.AddListener(delegate { OnExitButtonClick(); });

        if (saveButton != null)
        {
            saveButton.onClick.AddListener(delegate { OnSaveButtonClick(); });
        }
        if (loadButton != null)
        {
            loadButton.onClick.AddListener(delegate { OnLoadButtonClick(); });
        }

        resolutions = Screen.resolutions;
        foreach(Resolution resolution in resolutions)
        {
            resolutionDropdown.options.Add(new Dropdown.OptionData(resolution.ToString())); 
        }

        //LoadSettings();
    }

    public void OnFullscreenToggle()
    {
        Screen.fullScreen = gameSettings.fullscreen = fullscreenToggle.isOn;
    }

    public void OnResolutionChange()
    {
        Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height, Screen.fullScreen);
        gameSettings.resolutionIndex = resolutionDropdown.value;
    }

    public void OnTextureQualityChange()
    {
        QualitySettings.masterTextureLimit = gameSettings.textureQualiy = textureQualityDropdown.value;
    }

    public void OnAntialiasingChange()
    {
        QualitySettings.antiAliasing = gameSettings.antialiasing = (int)Mathf.Pow(2, antialiasingDropdown.value);
    }

    public void OnVSyncChange()
    {
        QualitySettings.vSyncCount = gameSettings.vSync = vSyncDropdown.value;
    }

    public void OnMusicVolumeChange()
    {
        musicSource.volume = gameSettings.musicVlolume = musicVolumeSlider.value;
    }

    public void OnApplyButtonClick()
    {
        SaveSettings();
    }

    public void OnExitButtonClick()
    {
        if (SceneManager.GetActiveScene().name != "ArtMainMenu")
        {
            SceneManager.LoadScene("ArtMainMenu");
        }
        else UIManager.Instance.SetSettingsMenuActive(false);
    }

    public void SaveSettings()
    {
        string jsonData = JsonUtility.ToJson(gameSettings, true);
        File.WriteAllText(Application.persistentDataPath + "/gameSettings.json", jsonData);
    }

    public void OnSaveButtonClick()
    {
        SaveSystem.Instance.SaveLastState();
    }

    public void OnLoadButtonClick()
    {
        UIManager.Instance.SetSettingsMenuActive(false);
        SaveSystem.Instance.LoadFromLastSave();
    }

    public void LoadSettings()
    {
        try
        {
            gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/gameSettings.json"));
        }catch(FileNotFoundException e)
        {
            Debug.Log(e);
            gameSettings = null;
        }
        if (gameSettings != null)
        {
            musicVolumeSlider.value = gameSettings.musicVlolume;
            antialiasingDropdown.value = gameSettings.antialiasing;
            vSyncDropdown.value = gameSettings.vSync;
            textureQualityDropdown.value = gameSettings.textureQualiy;
            resolutionDropdown.value = gameSettings.resolutionIndex;
            fullscreenToggle.isOn = gameSettings.fullscreen;
        }
        RefreshValues();
    }

    public void RefreshValues()
    {
        resolutionDropdown.RefreshShownValue();
        OnFullscreenToggle();
        OnResolutionChange();
        OnTextureQualityChange();
        OnAntialiasingChange();
        OnVSyncChange();
        OnMusicVolumeChange();
    }
}
