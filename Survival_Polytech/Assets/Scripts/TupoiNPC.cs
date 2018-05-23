using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class TupoiNPC : MonoBehaviour
{

    [Tooltip("How long NPC will stay in the new destination")]
    public float waitingTime = 1f;
    public Transform[] destinations;

    NavMeshAgent nav;
    Transform player;
    private bool newWay;
    private bool playerFollowing;
    private bool isStoped;
	private Animator anim;


    void Start()
    {
		anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        SetRandomDestination();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        newWay = false;
        playerFollowing = false;
        isStoped = false;
		anim.enabled = true;
    }

    void Update()
    {
        if (!isStoped)
        {
            if (!newWay && !playerFollowing && (nav.remainingDistance <= nav.stoppingDistance || nav.isStopped))
            {	
				anim.SetBool("IsWalking",false);
                StartCoroutine(newRandomDestination(waitingTime));
                newWay = true;
            }
            else
            {	
                if (playerFollowing)
                    nav.SetDestination(player.position);
            }
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }
    }

    private IEnumerator newRandomDestination(float value)
    {
        yield return new WaitForSeconds(value);
        SetRandomDestination();
        newWay = false;
		anim.SetBool("IsWalking", true);
    }

    public void SetRandomDestination()
    {
        nav.SetDestination(destinations[Random.Range(0, destinations.Length)].position);
		anim.SetBool("IsWalking", true);
    }

    public void SetDestination(int arrayIndex)
    {
        nav.SetDestination(destinations[arrayIndex].position);
		anim.SetBool("IsWalking", true);
    }

    public void SetCustomDestination(Transform newTarget)
    {
        nav.SetDestination(newTarget.position);
		anim.SetBool("IsWalking", true);
    }

    public void SetPlayerFollowing(bool isFollowing)
    {
        playerFollowing = isFollowing;
    }

    public void StopWalking()
    {
		anim.SetBool("IsWalking", false);
        nav.isStopped = true;
        isStoped = true;
    }

    public void ContinueWandering()
    {
        isStoped = false;
        nav.isStopped = false;
        SetRandomDestination();
    }
}