using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//https://answers.unity.com/questions/1490192/how-can-i-highlight-the-vive-controller-buttons.html
public class CTaskController : MonoBehaviour
{

    private Queue<CTask> tasklist;
    private CTask currentTask = null;
    private bool missionCompleted = false;

    [SerializeField] private List<GameObject> objectsToHide;
    public Text InformationText { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject obj in objectsToHide)
        {
            obj.SetActive(false);
        }

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
        foreach(GameObject obj in objectsToHide)
        {
            obj.SetActive(true);
        }

        CGameMaster.Instance.BackToMenuCanvasController.SetMainText("Level.MainMenu.Tutorial.Completed");
    }
}
