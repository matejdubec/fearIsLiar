using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CEnemyAgentDog : CEnemyAgentBase
{
    [SerializeField] private Transform[] destinations;
    private float distance = 0.0f;
    private int index = 0;

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
            distance = Vector3.Distance(this.transform.position, destination);
            agent.SetDestination(destination);
            if (distance < 1)
            {
                this.ChangeDestination();
            }

            if (!animator.GetBool("Run"))
            {
                animator.SetTrigger("Run");
            }
        }
    }

    public override void StandUp()
    {
        if(destinations.Length != 0)
        {
            isFollowing = true;
            destination = destinations[index].position;
        }
    }

    private void ChangeDestination()
    {
        if(index < destinations.Length)
        {
            destination = destinations[index].position;
            agent.SetDestination(destination);
            index++;
        }
        else
        {
            index = 0;
        }        
    }
}
