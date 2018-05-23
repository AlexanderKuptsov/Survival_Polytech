using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : Interactable {

    public Item key;    
    public bool open = false;    
    public float turnSpeed = 10f; 
    public Interactable goTo;

    private int way = -1;
    private bool interact = false;


    public void Update()
    {
        if ((interact) && (open))
        {
            Debug.Log(way);
            if ((way == 1) || (way == 2))
            {
                if (transform.rotation.y > -0.9)
                {
                    //while ((rotate - transform.rotation.y) * (rotate - transform.rotation.y) < 8100) // если знаешь как модуль сделать упрости плз
                    transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
                    way = 2;
                }
            }
            else if ((way == 0) || (way == 3))
            {
                if (transform.rotation.y < 0.9)
                {
                    //while ((rotate - transform.rotation.y) * (rotate - transform.rotation.y) < 8100) // почему то while не работает :(
                    transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
                    way = 3;
                }
            }
        }
        else
        {
            if (transform.rotation.y > 0)
                //while (transform.rotation.y != rotate)
                transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);

            if (transform.rotation.y < 0)
                //while (transform.rotation.y != rotate)
                transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (Inventory.Instance.Find(key) == true)
        {
            Inventory.Instance.Remove(key);
            open = true;
        }
        if (other.tag == "Player")
        {
            //rotate = transform.rotation.y;
            //Debug.Log(rotate);
            if (!open)
            {
                other.GetComponent<PlayerMovement>().FollowTarget(goTo);
         
            }
            else
            {
                interact = true;
                if ((transform.position.z < other.transform.position.z) && (way != 2) && (way != 3)) way = 0;
                if ((transform.position.z > other.transform.position.z) && (way != 2) && (way != 3)) way = 1;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (!open)
        {
            other.GetComponent<PlayerMovement>().FollowTarget(goTo);

        }
    }


        private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            interact = false;
            if (transform.position.z < other.transform.position.z) way = 0;
            else way = 1;
        }
    }

    void OnMouseEnter()
    {
        if (detect != null)
        {
            description.enabled = true;
            SetText();
            ForImageSet();
        }
    }

    private void OnMouseExit()
    {
        if (detect != null)
        {
            description.enabled = false;
            RemoveText();
            ForImageRemove();
        }
    }
}
