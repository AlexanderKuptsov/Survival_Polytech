using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour {

    [Tooltip("Loading scene")]
    public int sceneID;
    public Text progressText;

    private Slider loadingSlider;

    private void Start()
    {
        loadingSlider = GetComponent<Slider>();
        StartCoroutine(AsyncLoad());
    }

    IEnumerator AsyncLoad()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);
        while (!operation.isDone)
        {
            float progress = operation.progress / 0.9f;
            loadingSlider.value = progress;
            progressText.text = string.Format("{0:0}%", progress * 100);
            yield return null;
        }
    }

}
