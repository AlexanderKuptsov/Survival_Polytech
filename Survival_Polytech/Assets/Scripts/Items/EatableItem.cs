using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/EatableItem")]
public class EatableItem : Item {

    [Header("Effects")]
    public float health;
    public float eat;
    public float sleepiness;
    public int earnedEXP;
    
   override public void Use()
    {
        base.Use();
        Debug.Log("Eat " + name);
        removeFromInventory();
        FloatingTextController.CreateFloatingText("+" + earnedEXP, GameObject.FindGameObjectWithTag("Player").transform);

        CharacterStats.Instance.HealthChange(health);
        CharacterStats.Instance.Starvation(-eat);
        CharacterStats.Instance.FallingAsleep(-sleepiness);
        CharacterStats.Instance.EarnEXP(earnedEXP);

       // NoteManager.Instance.OpenNewNote("Food \n\n\n Press 'T' to win");        
    }
}
