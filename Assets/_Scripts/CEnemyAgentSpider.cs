using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum EMovingState
{
    Walking,
    Running,
}

public class CEnemyAgentSpider : CEnemyAgentBase
{
    [SerializeField] private float minWalkDistance = 4f;
    [SerializeField] private float maxWalkDistance = 8f;
    private float changeDestinationOffset = 1f;
    private float playerDistanceOffset = 3f;
    private float timeToChangeDestination = 2f;

    private EMovingState currentState;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        destination = GetRandomNavmeshPosition(Random.Range(minWalkDistance, maxWalkDistance));
        agent.SetDestination(destination);
        currentState = EMovingState.Walking;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistanceFromPlayer();
        float distance = Vector3.Distance(transform.position, destination);

        if(distance < changeDestinationOffset || timeToChangeDestination < 0f)
        {
            destination = GetRandomNavmeshPosition(Random.Range(minWalkDistance, maxWalkDistance));
            agent.SetDestination(destination);
            timeToChangeDestination = Random.Range(5f, 10f);
        }

        timeToChangeDestination -= Time.deltaTime;
    }


    private Vector3 GetRandomNavmeshPosition(float walkDistance)
    {
        bool positionFound = false;
        Vector3 randomDirection = Random.insideUnitSphere * walkDistance;
        randomDirection += transform.position;
        NavMeshHit hit = new NavMeshHit();

        while(!positionFound)
        {
            positionFound = NavMesh.SamplePosition(randomDirection, out hit, walkDistance, 1);
        }

        return hit.position;
    }
    
    private void CheckDistanceFromPlayer()
    {
        float distance = Vector3.Distance(transform.position, CGameManager.Instance.Player.transform.position);

        if(distance < playerDistanceOffset)
        {
            SetRunningState();
        }
        else
        {
            SetWalkingState();
        }
    }

    private void SetRunningState()
    {
        if(currentState != EMovingState.Running)
        {
            currentState = EMovingState.Running;
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", true);
            agent.speed = 1f;
            agent.angularSpeed = 1000;
        }
    }

    private void SetWalkingState()
    {
        if(currentState != EMovingState.Walking)
        {
            currentState = EMovingState.Walking;
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", true);
            agent.speed = 0.2f;
            agent.angularSpeed = 120;
        }
    }
}
