using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CEnemyAgentZombie : CEnemyAgentBase
{

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

    public override void StandUp()
    {
        animator.SetBool("stand", true);
        isFollowing = true;
    }
}
