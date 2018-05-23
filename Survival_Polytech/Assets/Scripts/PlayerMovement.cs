using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour{
	private Animator anim;
    public Interactable focus;

    Transform target; // for not static focuses
    NavMeshAgent mesh;

    public bool IsStopped { get; set; }

    void Start()
    {
        mesh = GetComponent<NavMeshAgent>();
		anim = GetComponent<Animator>();
		anim.enabled = true;

        IsStopped = false;
    }


    void Update()
    {
        if (!IsStopped)
        {
            if (EventSystem.current.IsPointerOverGameObject())   // чтобы когда открыто что то на UI он не ходил при клике на UI
                return;

            if (target != null)
            {
                MoveToPoint(target.position);
                anim.SetBool("IsMoving", true);
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000) && Input.GetMouseButtonDown(0))
            {                
                Interactable inter = hit.collider.GetComponent<Interactable>();
                if (inter != null)
                {
                    mesh.SetDestination(transform.position);
                    SetFocus(inter);
                }
                else
                {
                    MoveToPoint(hit.point);
                    RemoveFocus();
                }
                anim.SetBool("IsMoving", true);
            }
            else
            {

                if (mesh.remainingDistance * 0.85f <= mesh.stoppingDistance || mesh.isStopped)
                {
                    anim.SetBool("IsMoving", false);
                    //Debug.Log(anim.GetBool("IsMoving"));
                }
            }
        }
        else
        {
            anim.SetBool("IsMoving", false);  ////// ...
        }
    }

    public void MoveToPoint(Vector3 point)
    {
		//anim.SetBool("IsMoving", true);
        mesh.stoppingDistance = 0f;     // for safety
        mesh.SetDestination(point);	
    }

    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
                focus.OnDefocused();
            focus = newFocus;
            FollowTarget(newFocus);
        }
        newFocus.OnFocused(transform);

        //MoveToPoint(newFocus.transform.position); не получится если объект в движении
    }

    private void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();
        focus = null;
        StopFollowing();
    }

    public void FollowTarget(Interactable newTarget)
    {
        mesh.stoppingDistance = newTarget.radius * 0.8f;
        target = newTarget.interactTransform;
    }

    public void StopFollowing()
    {
        mesh.stoppingDistance = 0f;
        target = null;
    }
}
