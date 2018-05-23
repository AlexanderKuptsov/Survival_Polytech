using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal  {

    public Quest Quest { get; set; }
    public string Description { get; set; }
    public bool Completed { get; set; }
    public int CurrentAmount { get; set; }
    public int RequiredAmount { get; set; }

    private bool initialized = false;
    public bool Initialized
    {
        get
        {
            return initialized;
        }
        private set
        {
            initialized = value;
        }
    }

    public virtual void Init()
    {
        Initialized = true;
        // default initialize stuff
    }

    public void Evaluate()
    {
        if (CurrentAmount >= RequiredAmount)
        {
            Complete();
        }
    }

    public virtual void Complete()
    {
        Quest.CheckGoals();
        if (!Quest.Completed)
            Quest.ActivateNextGoal();

        Completed = true;
        Debug.Log("Goal marked as completed.");
    }
}
