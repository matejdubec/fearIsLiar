using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTaskController : MonoBehaviour
{

    private Queue<CTask> tasklist;
    private CTask currentTask = null;
    private bool missionCompleted = false;

    // Start is called before the first frame update
    void Start()
    {
        tasklist = new Queue<CTask>();
        foreach(Transform child in this.transform)
        {
            CTask task = child.GetComponent<CTask>();

            if (task)
            {
                task.Init();
                tasklist.Enqueue(task);
            }
        }
        NextTask();
    }

    private void NextTask()
    {
        if (tasklist.Count > 0)
        {
            currentTask = tasklist.Dequeue();
            currentTask.Activate();
            currentTask.TaskDone += OnNextTask;
        }
        else
        {
            MissionCompleted();
        }
    }

    private void OnNextTask(object sender, EventArgs e)
    {
        currentTask.TaskDone -= OnNextTask;
        currentTask.Deactivate();
        NextTask();
    }


    private void MissionCompleted()
    {
        Debug.LogError("Mission completed");
    }
}
