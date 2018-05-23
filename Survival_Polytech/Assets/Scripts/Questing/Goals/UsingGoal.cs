using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsingGoal : Goal {

    public Item Item { get; set; }

    public UsingGoal(Quest quest, Item item, string description, bool completed, int currentAmount, int requiredAmount)
    {
        this.Quest = quest;
        this.Item = item;
        this.Description = description;
        this.Completed = completed;
        this.CurrentAmount = currentAmount;
        this.RequiredAmount = requiredAmount;
    }

    public override void Init()
    {
        base.Init();
        EventHandler.OnItemUsed += ItemUsed;
    }

    void ItemUsed(Item item)
    {
        if (item == this.Item)
        {
            this.CurrentAmount++;
            Evaluate();
        }
    }

    public override void Complete()
    {
        base.Complete();
        EventHandler.OnItemUsed -= ItemUsed;
    }
}
