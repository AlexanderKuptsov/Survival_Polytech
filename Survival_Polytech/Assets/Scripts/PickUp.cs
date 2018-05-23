using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : Interactable {

    public Image takeble;
    public Item item;
    public int cost = 50;

    private void Start()
    {
        if (takeble == null && base.GetDefaultDescription() != null)
            takeble = base.GetDefaultDescription().transform.GetChild(1).GetComponent<Image>();
    }

    public override void Interact()
    {
        base.Interact(); // берет всё из оригинала
       
        if (CharacterStats.Instance.ChangeMoney(-cost))
            Take();
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
