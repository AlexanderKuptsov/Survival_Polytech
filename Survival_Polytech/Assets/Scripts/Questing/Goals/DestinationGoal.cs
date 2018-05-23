using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationGoal : Goal {

    private string goalPositionID = "0";

    public DestinationGoal(Quest quest, string _goalPositionID, string description, bool completed, int currentAmount, int requiredAmount)
    {
        this.Quest = quest;
        this.goalPositionID = _goalPositionID;
        this.Description = description;
        this.Completed = completed;
        this.CurrentAmount = currentAmount;
        this.RequiredAmount = requiredAmount;

        EventHandler.OnTriggerEnter += CheckPosition;
    }

    public override void Init()
    {
        base.Init();
    }    

    void CheckPosition(string triggerID)
    {
        if (goalPositionID == triggerID)
        {
            Debug.Log("Destination reached");
            CurrentAmount++;
            Evaluate();
        }            
    }

    void DestinationReached()
    {

    }

    public override void Complete()
    {
        base.Complete();
        EventHandler.OnTriggerEnter -= CheckPosition;
    }
}
