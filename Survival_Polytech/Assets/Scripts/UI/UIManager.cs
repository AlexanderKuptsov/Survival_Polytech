using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    #region Singleton

    public static UIManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Slomalsya Uai chini daun");
            return;
        }
        Instance = this;
		FloatingTextController.Initialize();
        Time.timeScale = 1;
        settingsMenu.SetActive(true);
    }

    #endregion

    public GameObject settingsMenu;

    private float timeScale = 1f;
    

    void Start()
    {        
        settingsMenu.SetActive(false);
    }
       
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            settingsMenu.SetActive(!settingsMenu.activeSelf);
            if (settingsMenu.activeSelf) Time.timeScale = 0;
            else Time.timeScale = timeScale;

            EventHandler.TimeScaleChanged(Time.timeScale);
        }
    }

    public void SetSettingsMenuActive(bool isActive)
    {
        settingsMenu.SetActive(isActive);
        if (settingsMenu.activeSelf) Time.timeScale = 0;
        else Time.timeScale = timeScale;

        EventHandler.TimeScaleChanged(Time.timeScale);
    }
}
