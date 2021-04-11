using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CEnemyAgentZombie : CEnemyAgentBase
{
    private const float timeToSaySomething = 20f;
    private float timeToSaySomethingCounter = timeToSaySomething;


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

            if(timeToSaySomethingCounter <= 0f && !CGameManager.Instance.AudioManager.IsPlaying("ZombieBrains"))
            {
                CGameManager.Instance.AudioManager.PlaySound("ZombieBrains");
                timeToSaySomethingCounter = timeToSaySomething;
            }

            timeToSaySomethingCounter -= Time.deltaTime;
        }
    }

    public override void StandUp()
    {
        if(!isFollowing)
        {
            animator.SetBool("stand", true);
            isFollowing = true;
            CGameManager.Instance.AudioManager.PlaySound("ZombieBrains");
        }
    }
}
