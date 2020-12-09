using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class CGetTaskItem : MonoBehaviour
{
    private Interactable interactable = null;
    [SerializeField] private CWaypoint task;
    [SerializeField] private GameObject objectToCollideWith;

    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
        interactable.IsHeld += IsHeld;
    }

    private void IsHeld(object sender, EventArgs e)
    {
        task.TaskCompleted();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == objectToCollideWith)
        {
            if(task.gameObject.activeSelf)
                task.TaskCompleted();
        }
    }
}
