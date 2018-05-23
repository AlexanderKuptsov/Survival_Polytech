using GreatArcStudios;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{

    public GameObject settingsPanel;


    private void OnMouseDown()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }
}
