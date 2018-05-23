using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour {

    public static DialogueSystem Instance { get; set; }

    public GameObject dialoguePanel;

    [HideInInspector]
    public string npcName = "???";
    [HideInInspector]
    public List<string> dialogueLines = new List<string>();

    Button continueButton;
    Text dialogueText, nameText;
    int dialogueIndex;

    int numberOfAnswersPerQuestion;
    string[][] answers;
    int[] indexOfAnswerArray;
    int indexOfAnswer;
    bool answering;

    Interactable talker;

	
	void Awake () {
        answering = false;

        continueButton = dialoguePanel.transform.Find("Continue").GetComponent<Button>();
        continueButton.onClick.AddListener(delegate { ContinueDialogue(); });

        dialogueText = dialoguePanel.transform.Find("Text").GetComponent<Text>();
        nameText = dialoguePanel.transform.Find("Name").GetChild(0).GetComponent<Text>();
        dialoguePanel.SetActive(false);

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
	}
	
	public void AddNewMonologue(string npcName, string[] lines)
    {
        this.dialogueIndex = 0;
        this.npcName = npcName;
        dialogueLines = new List<string>(lines.Length);
        dialogueLines.AddRange(lines);

        CreateDialogue();
    }

    public void AddNewMonologue(string npcName, string[] lines, Interactable talker)
    {
        this.dialogueIndex = 0;
        this.talker = talker;
        this.npcName = npcName;
        dialogueLines = new List<string>(lines.Length);
        dialogueLines.AddRange(lines);
        
        CreateDialogue();
    }

    public void AddNewDialogue(string npcName, string[] lines, string[][] answers, int[] indexOfAnswer, int numberA)
    {
        this.dialogueIndex = 0;
		this.indexOfAnswer = 0;
        this.npcName = npcName;
        dialogueLines = new List<string>(lines.Length);
        dialogueLines.AddRange(lines);

        this.answers = answers;
        this.indexOfAnswerArray = indexOfAnswer;
        this.indexOfAnswer = 0;
        this.numberOfAnswersPerQuestion = numberA;

        CreateDialogue();
        CheckAnswerRequirement();
    }

    public void AddNewDialogue(string npcName, string[] lines, Interactable talker, string[][] answers, int[] indexOfAnswers, int numberA)
    {
        this.dialogueIndex = 0;
		this.indexOfAnswer = 0;
        this.talker = talker;
        this.npcName = npcName;
        dialogueLines = new List<string>(lines.Length);
        dialogueLines.AddRange(lines);

        this.answers = answers;
        this.indexOfAnswerArray = indexOfAnswers;
        this.indexOfAnswer = 0;
        this.numberOfAnswersPerQuestion = numberA;

        CreateDialogue();
        CheckAnswerRequirement();
    }

    public void CreateDialogue()
    {
        nameText.text = npcName;
        dialogueText.text = dialogueLines[dialogueIndex];
        dialoguePanel.SetActive(true);
    }

    public void ContinueDialogue()
    {
        if (!answering)
        {
            if (dialogueIndex < dialogueLines.Count - 1)
            {
                dialogueIndex++;
                dialogueText.text = dialogueLines[dialogueIndex];

                CheckAnswerRequirement();
            }
            else
            {
                if (talker != null)
                {
                    talker.AfterInteract();
                    talker = null;
                }
                dialoguePanel.SetActive(false);
                Debug.Log("End of conversation");
            }
        }
    }

    private void CheckAnswerRequirement()
    {
        if (indexOfAnswer < indexOfAnswerArray.Length)
        {
            if (dialogueIndex == indexOfAnswerArray[indexOfAnswer])
            {
                CreateAnswer(answers[indexOfAnswer]);
                indexOfAnswer++;
            }
        }
    }

    public void CreateAnswer(string[] answerLines)
    {
        AnsweringSystem.Instance.AddNewAnswer(answerLines, numberOfAnswersPerQuestion);
        answering = true;
        continueButton.interactable = false;
    }

    public void EndAnswer(int answerIndex)
    {
        answering = false;
        continueButton.interactable = true;
        AfterAnsweringAction(answerIndex);
        ContinueDialogue();
    }

    public void AfterAnsweringAction(int answerI)
    {
        // разветвление диалога (answerIndex)
		if (answerI != 0) answerI = 1;
        dialogueIndex += answerI; // простое разветвление
        if (answerI == 0)
        {
            // event
        }
    }
}
