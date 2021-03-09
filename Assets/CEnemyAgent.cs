using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CEnemyAgent : MonoBehaviour
{
    private NavMeshAgent agent = null;
    private Vector3 destination;
    private bool isFollowing = false;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isFollowing)
        {
            var distance = Vector3.Distance(this.transform.position, CGameManager.Instance.Player.transform.position);

            if(distance > agent.stoppingDistance)
            {
                agent.SetDestination(CGameManager.Instance.Player.transform.position);
                if(!animator.GetBool("walking"))
                {
                    animator.SetBool("walking", true);
                }
            }
            else
            {
                if (animator.GetBool("walking"))
                {
                    animator.SetBool("walking", false);                   
                }
            }
        }
    }

    public void StandUp()
    {
        animator.SetBool("stand", true);
        isFollowing = true;
    }
}
