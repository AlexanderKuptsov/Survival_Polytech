using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeIsLife : Quest {

    public GameObject goalPosition;
    public Item coffeeItem;


    private void OnEnable()
    {
        Debug.Log("Coffee rush!");
        QuestName = "Coffee is your life";
        Description = "Try not to fall asleep";
        //ItemReward = ItemDatabase.Instance.GetItem(" ");
        ExperienceReward = 450;

        Goals = new List<Goal>
        {
            new DestinationGoal(this, goalPosition.GetComponent<TriggerDestination>().triggerID, "Find cafe", false, 0, 1),
            new CollectionGoal(this, "coffee_1", "Take coffee", false, 0, 1),
            new UsingGoal(this, coffeeItem, "Drink some coffee", false, 0, 1)
        };

        GoalIndex = 0;
        ActivateGoal(0);
    }
}
