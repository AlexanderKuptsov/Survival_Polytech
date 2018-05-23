using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esc : MonoBehaviour {

    private void OnMouseDown()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
