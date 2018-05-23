using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    #region Singleton

    public static Inventory Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Slomalsya inventar' chini daun");
            return;
        }
        Instance = this;
    }

    #endregion
    
    public int space = 20;

    public List<Item> items = new List<Item>();
    

    public bool Add(Item item)
    {
        if (!item.isDefaultItem)
        {
            if (items.Count >= space)
            {
                Debug.Log("Inventory more than full idiot");
                return false;
            }

            items.Add(item);

            EventHandler.ItemAddedToInventory(item);
        }
        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);

        EventHandler.ItemRemovedFromInventory(item);
    }

    public void RemoveByID(string ID)
    {
        if (items.Count != 0)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == ID) Remove(items[i]);
            }
        }
    }

    public void RemoveAll()
    {
        if (items.Count != 0)
        {
            List<Item> itemsToRemove = new List<Item>(items);
            foreach (Item i in itemsToRemove)
            {
               if (i != null) Remove(i);
            }
        }
    }

    public bool Find(Item item)
    {
        if (items.Count != 0)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] == item) return true;
            }
        }
        return false;
    }

    public bool FindWithID(string ID)
    {
        if (items.Count != 0)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == ID) return true;
            }
        }
        return false;
    }
}
