using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Thinking : MonoBehaviour {

    #region Singleton

    public static Thinking Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Slomalis' misli chini daun");
            return;
        }
        Instance = this;
    }

    #endregion

    public string[] dialogue;

    private string NpcName = "You";

    private NavMeshAgent playerNavMesh;
    private PlayerMovement playerMovement;

    private void Start()
    {        
        playerNavMesh = GetComponent<NavMeshAgent>();
        playerMovement = GetComponent<PlayerMovement>();

        StartThinking(0);
    }

    public void StartThinking(int indexOfDialogue)
    {
        DialogueSystem.Instance.AddNewMonologue(NpcName, new string[] { dialogue[indexOfDialogue] } );

        //playerNavMesh.isStopped = true;
        Debug.Log("Thinking..");
    }
}
