using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Quest : MonoBehaviour {

    private int level;
    public int Level
    {
        get
        {
            return level;
        }
       private set
        {
            level = value;
        }
    }

    public List<Goal> Goals { get; set; }
    public string QuestName { get; set; }
    public string Description { get; set; }
    public int ExperienceReward { get; set; }
    public Item ItemReward { get; set; }
    public bool Completed { get; set; }
    public bool RewardOnComplete { get; set; }

    public int GoalIndex { get; set; } 

    private void Start()
    {
        Goals = new List<Goal>();
        GoalIndex = 0;
    }

    public void CheckGoals()
    {        
        Completed = Goals.All(g => g.Completed);
        if (Completed)
        {
            Done();
            if (RewardOnComplete) GiveReward();
        }       
    }

    public void ActivateNextGoal()
    {
        GoalIndex++;
        if (GoalIndex < Goals.Count)
            Goals[GoalIndex].Init();
    }

    public void ActivateGoal(int index)
    {
        if (index >= 0 && index < Goals.Count)
            if (!Goals[index].Initialized)
                Goals[index].Init();

    }

    public void ActivateAllGoals()
    {
        Goals.ForEach(g => g.Init());
    }

    public virtual void Done()
    {	
		
        QuestSystem.Instance.CheckQuests();
        // Quest result
    }

    public void GiveReward()
    {
        CharacterStats.Instance.EarnEXP(ExperienceReward);

        if (ItemReward != null)
            Inventory.Instance.Add(ItemReward);
    }
}
