using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eating : Quest {

    public Item food;


    private void OnEnable()
    {	
		RewardOnComplete = true;
        Debug.Log("Еда!");
        QuestName = "Eating";
        Description = "Найди еду";
        //ItemReward = ItemDatabase.Instance.GetItem(" ");
        ExperienceReward = 450;

        Goals = new List<Goal>
        {
            new UsingGoal(this, food, "Найди еды", false, 0, 1)
        };

        GoalIndex = 0;
        ActivateGoal(0);
    }
	
	public override void Done() 
	{
		base.Done();
		QuestSystem.Instance.currentQuest = QuestSystem.Instance.Quests[1];
	}
}
