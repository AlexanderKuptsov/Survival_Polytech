using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosingStatsUI : MonoBehaviour {

	public GameObject Panel;	
	public string openAnim;
	public string closeAnim;
	public string StringKeyCode;

    private Animator anime;
    private KeyCode kc;
    private bool isOpened;
    private bool animating;

	// Use this for initialization
	void Start () {
		Panel.gameObject.SetActive (false);
		anime = Panel.GetComponent<Animator> ();
		anime.enabled = false;        
		kc = (KeyCode)System.Enum.Parse (typeof(KeyCode), StringKeyCode);
        isOpened = false;
        animating = false;
    }

	// Update is called once per frame
	void Update () {
        if (!animating)
        {
            if (Input.GetKeyDown(kc) && !isOpened)
            {
                OpenMenu();
            }
            else if (Input.GetKeyDown(kc) && isOpened)
            {
                CloseMenu();
            }
        }
	}

	public void OpenMenu() {
        animating = true;
		Panel.gameObject.SetActive (true);
		anime.enabled = true;	
		anime.Play (openAnim);
		StartCoroutine (WaitForOpen ());
	}

	public void CloseMenu() {
        animating = true;
        anime.Play (closeAnim);
		StartCoroutine (WaitForClose ());
	}

	private IEnumerator WaitForOpen() {
		yield return new WaitForSeconds (1);
		isOpened = true;
        animating = false;
    }

	private IEnumerator WaitForClose() {
		yield return new WaitForSeconds (1);
		Panel.gameObject.SetActive (false);
		isOpened = false;
        animating = false;
    }
}