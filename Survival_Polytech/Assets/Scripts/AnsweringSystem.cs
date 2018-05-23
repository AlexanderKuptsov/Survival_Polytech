using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnsweringSystem : MonoBehaviour {
    public static AnsweringSystem Instance { get; set; }

    public GameObject answerPanel;
    //public GameObject choice1, choice2;

    [HideInInspector]
    public List<string> answerLines = new List<string>();

    public GameObject choiceButton11;
    public GameObject choiceButton22;
    public GameObject choiceButton33;
    public GameObject choiceButton44;
	
	private Button choiceButton1;
    private Button choiceButton2;
    private Button choiceButton3;
    private Button choiceButton4;

    Text answerText1, answerText2, answerText3, answerText4;

    Interactable talker;


    void Awake()
    {	
		choiceButton1 = choiceButton11.GetComponentInChildren<Button>();
		choiceButton2 = choiceButton22.GetComponentInChildren<Button>();
		choiceButton3 = choiceButton33.GetComponentInChildren<Button>();
		choiceButton4 = choiceButton44.GetComponentInChildren<Button>();
        //choiceButton1 = choice1.GetComponentInChildren<Button>();
        choiceButton1.onClick.AddListener(delegate { ContinueDialogue(0); });


        //choiceButton2 = choice2.GetComponentInChildren<Button>();
        choiceButton2.onClick.AddListener(delegate { ContinueDialogue(1); });

        choiceButton2.onClick.AddListener(delegate { ContinueDialogue(2); });
        choiceButton2.onClick.AddListener(delegate { ContinueDialogue(3); });

        answerText1 = choiceButton1.GetComponentInChildren<Text>();
        answerText2 = choiceButton2.GetComponentInChildren<Text>();
        answerText3 = choiceButton3.GetComponentInChildren<Text>();
        answerText4 = choiceButton4.GetComponentInChildren<Text>();

        answerPanel.SetActive(false);

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void AddNewAnswer(string[] lines, int numberA)
    {

        answerLines = new List<string>(lines.Length);
        answerLines.AddRange(lines);
        Debug.Log("new answer");
        CreateAnswer(numberA);
    }

    public void CreateAnswer(int numberA)
    {	
		choiceButton11.SetActive(true);
		choiceButton22.SetActive(true);
		Debug.Log("YAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" + numberA);
		if (numberA == 4) 
		{ 
		choiceButton33.SetActive(true); 
		choiceButton44.SetActive(true); 
		}
		else 
		{ 
		choiceButton33.SetActive(false); 
		choiceButton44.SetActive(false); 
		}
        answerText1.text = answerLines[0];
        answerText2.text = answerLines[1];
        if (numberA == 4)
        {
            answerText3.text = answerLines[0];
            answerText4.text = answerLines[1];
        }
        answerPanel.SetActive(true);
    }

    public void ContinueDialogue(int answerIndex)
    {
        answerPanel.SetActive(false);
        Debug.Log("End of answer");
		DialogueSystem.Instance.EndAnswer(answerIndex);
    }
}
