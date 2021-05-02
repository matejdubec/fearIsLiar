using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CEnemyAgentDog : CEnemyAgentBase
{
    [SerializeField] private Transform disableDestination;
    private float distance = 0.0f;
    private int index = 0;

    // Update is called once per frame
    void Update()
    {
        if(isFollowing)
        {
            distance = Vector3.Distance(this.transform.position, destination);
            agent.SetDestination(destination);
            if (distance < 1)
            {
                this.gameObject.SetActive(false);
            }

            if (!animator.GetBool("Run"))
            {
                animator.SetTrigger("Run");
            }
        }
    }

    public override void StandUp()
    {
        if(disableDestination && !isFollowing)
        {
            isFollowing = true;
            destination = disableDestination.position;
            CGameManager.Instance.AudioManager.PlaySound("ZombieBark");
        }
    }
}