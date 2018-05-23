using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryingItem : MonoBehaviour {

    public GameObject CarryingHand;
    public GameObject EquipedItem;

    public void Equip(Item newItemToEquip)
    {
        if (EquipedItem != null)
        {
            Destroy(CarryingHand.transform.GetChild(0).gameObject);
        }

        EquipedItem = (GameObject)Instantiate(Resources.Load<GameObject>("Items" + newItemToEquip.name), CarryingHand.transform.position, CarryingHand.transform.rotation);
        EquipedItem.transform.SetParent(CarryingHand.transform);

        Debug.Log("Smth was equiped");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && EquipedItem != null)
        {
            //EquipedItem.GetComponent<Item>().PerformAction();
        }
    }

    public void PerformAction()
    {
        // smth cool
    }
}
