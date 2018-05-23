using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour {

    public Transform itemsParent;
    public GameObject inventiryUI;

    Inventory inventory;

    InventorySlot[] slots;

	// Use this for initialization
	void Awake () {
        inventory = Inventory.Instance;
        //inventory.onItemChangedCalledBack += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
	}

    private void OnEnable()
    {        
        EventHandler.OnItemAddedToInventory += UpdateUI;
        EventHandler.OnItemRemovedFromInventory += UpdateUI;

        if (inventory == null) inventory = Inventory.Instance;
        else UpdateUI(null);
    }

    private void OnDisable()
    {
        EventHandler.OnItemAddedToInventory -= UpdateUI;
        EventHandler.OnItemRemovedFromInventory -= UpdateUI;
    }

    void UpdateUI(Item nullItem)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
