using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestQuest : Interactable {
    
    private int expAmount;

    void Start()
    {
        FloatingTextController.Initialize();
        expAmount = 2500;
    }

    public override void Interact()
    {
        if (Inventory.Instance.items.Count >= 2)
        {
            if (gameObject != null)
            {                
                FloatingTextController.CreateFloatingText("EXP +" + expAmount.ToString(), transform);
                CharacterStats.Instance.EarnEXP(2500);
                SaveSystem.Instance.AddToObjectsToDel(gameObject);
                Destroy(gameObject);
            }
        }
    }
}
