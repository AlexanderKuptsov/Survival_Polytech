using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpKey : Interactable {

    public Image takeble;
    public Item item;
    public int amount = 1;
    public GameObject quests;
    public GameObject key;

    private void Start()
    {
        if (takeble == null && base.GetDefaultDescription() != null)
            takeble = base.GetDefaultDescription().transform.GetChild(1).GetComponent<Image>();
    }

    public override void Interact()
    {
        base.Interact(); // берет всё из оригинала
        if (!QuestSystem.Instance.Quests[1].Completed && amount > 0 && quests.GetComponent<KeyQuest>().enabled == true)
        {
            Take();
            if (amount <= 0) key.SetActive(false);
        }
    }

    public override void ForImageSet()
    {
        //  takeble.enabled = true;
    }

    public override void ForImageRemove()
    {
        //  takeble.enabled = false;
    }

    void Take()
    {
        bool wasPickedUp = Inventory.Instance.Add(item);
        amount--;
        if (wasPickedUp)
        {
            ForImageRemove();
            description.enabled = false;
            text.enabled = false;
            RemoveText();
            // SaveSystem.Instance.AddToObjectsToDel(gameObject);
            // Destroy(gameObject);
        }
    }
}
