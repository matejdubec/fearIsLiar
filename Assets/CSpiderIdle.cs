using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSpiderIdle : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Hand"))
        {
            animator.SetBool("doAttack", true);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Hand"))
        {
            animator.SetBool("doAttack", false);
        }
    }
}
