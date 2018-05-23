using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyQuest : Quest {

    public Item key;


    private void OnEnable()
    {
		RewardOnComplete = false;

        Debug.Log("Найди ключ!");
        QuestName = "KeyQuest";
        Description = "Попробуй найти ключ";
        //ItemReward = ItemDatabase.Instance.GetItem(" ");
        ExperienceReward = 750;

        Goals = new List<Goal>
        {
            new CollectionGoal(this, "key1", "Попробуй найти ключ", false, 0, 1)
        };

        GoalIndex = 0;
        ActivateGoal(0);
    }
}