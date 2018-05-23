using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : Interactable {

    public string NpcName = "???";
    public string[] dialogue;

    public int numberOfAnswersPerQuestion = 2;
    public string[] allAnswers;
    public int[] indexOfAnswers;

    private TupoiNPC tupoiNPC;

    private GameObject hero;
    private NavMeshAgent playerNavMesh;
    private PlayerMovement playerMovement;

    [HideInInspector]
    public string[][] Answers { get; set; }
    private bool isInteracting = false;

    private void Start()
    {        
        tupoiNPC = GetComponent<TupoiNPC>();
        hero = GameObject.FindGameObjectWithTag("Player");
        playerNavMesh = hero.GetComponent<NavMeshAgent>();
        playerMovement = hero.GetComponent<PlayerMovement>();
        descriptionText = NpcName;

        int n = numberOfAnswersPerQuestion;
        if (n != 2 || n != 4) n = 2;

        Answers = new string[allAnswers.Length / n][];
        for (int i = 0; i < allAnswers.Length / n; i++)
        {
            Answers[i] = new string[n];
            Answers[i][0] = allAnswers[n * i];
            Answers[i][1] = allAnswers[n * i + 1];
            if (n == 4)
            {
                Answers[i][2] = allAnswers[n * i + 2];
                Answers[i][3] = allAnswers[n * i + 3];
            }
        }
    }

    private void LateUpdate()
    {
       // if (isInteracting)
           // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(hero.transform.position), 0.5f * Time.deltaTime); // rotation...
    }

    public override void Interact()
    {
        //DialogueSystem.Instance.AddNewMonologue(NpcName, dialogue, this);
        DialogueSystem.Instance.AddNewDialogue(NpcName, dialogue, this, Answers, indexOfAnswers, numberOfAnswersPerQuestion);
		if (tupoiNPC != null) 
		{
        tupoiNPC.StopWalking();
		}
        // rotation...
        isInteracting = true;
        //
        playerNavMesh.isStopped = true;
        playerMovement.IsStopped = true;
        Debug.Log("Interacting with " + NpcName);
    }

    public override void AfterInteract()
    {
		if (tupoiNPC != null) 
		{
        tupoiNPC.ContinueWandering();
		}
        playerMovement.StopFollowing();
        playerNavMesh.ResetPath();
        playerNavMesh.isStopped = false;
        playerMovement.IsStopped = false;
        isInteracting = false;
    }

    void OnMouseEnter()
    {
        if (detect != null)
        {
            description.enabled = true;
            SetText();
            ForImageSet();
        }
    }

    private void OnMouseExit()
    {
        if (detect != null)
        {
            description.enabled = false;
            RemoveText();
            ForImageRemove();
        }
    }
}
