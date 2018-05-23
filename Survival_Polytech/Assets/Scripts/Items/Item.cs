using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem" , menuName = "Inventory/Item")]
public class Item : ScriptableObject {

    new public string name = "New Item";
    public string ID = "";
    public Sprite icon = null;
    public bool isDefaultItem = false;

  
    public void removeFromInventory()
    {
        Inventory.Instance.Remove(this);
    }

    public virtual void Use()
    {
        Debug.Log("USING " + name);
        EventHandler.ItemUsed(this);
    }

    public virtual void PerformAction()
    {
        Debug.Log("Doing some cool things with " + name);
    }
}
