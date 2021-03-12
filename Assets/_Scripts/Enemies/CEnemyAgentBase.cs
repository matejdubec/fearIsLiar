using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public abstract class CEnemyAgentBase : MonoBehaviour
{
    protected NavMeshAgent agent = null;
    protected Vector3 destination;
    protected bool isFollowing = false;
    protected Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void StandUp()
    {
    }
}
