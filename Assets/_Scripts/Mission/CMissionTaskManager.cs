using System.Collections.Generic;
using UnityEngine;


//https://answers.unity.com/questions/1490192/how-can-i-highlight-the-vive-controller-buttons.html
public class CMissionTaskManager : MonoBehaviour
{
    [SerializeField] private EMissionId missionId;
    public EMissionId MissionId { get { return missionId; } }
    [SerializeField] private CMarker marker;

    private CLevelManager levelManager;

    [SerializeField] private List<GameObject> objectsToDeactivateOnMissionStart;
    [SerializeField] private List<GameObject> objectsToActivateOnMissionEnd;

    private Queue<CMissionTaskBase> taskQueue;
    private CMissionTaskBase currentTask = null;

    private bool completionState = false;
    public bool IsSuccessfullyDone { get { return completionState; } }

    public void Init(CLevelManager mManager)
	{
        levelManager = mManager;
        DeactivateOnMissionStart();

        var temp = Instantiate(marker.gameObject, this.transform);
        marker = temp.GetComponent<CMarker>();

        taskQueue = new Queue<CMissionTaskBase>();
        foreach (Transform child in this.transform)
        {
            CMissionTaskBase task = child.GetComponent<CMissionTaskBase>();

            if (task)
            {
                task.Init(this);
                taskQueue.Enqueue(task);
            }
            else
            {
                CMissionTaskBase childTask = child.GetComponentInChildren<CMissionTaskBase>();
                if (childTask)
                {
                    childTask.Init(this);
                    taskQueue.Enqueue(childTask);
                }
            }
        }        
    }

	public void StartMission()
    {
        NextTask();
    }

    private void NextTask()
    {
        if (taskQueue.Count > 0)
        {
            currentTask = taskQueue.Dequeue();
            currentTask.Activate();

            Vector3 markerPos = new Vector3(
                currentTask.transform.position.x, 
                currentTask.transform.position.y + currentTask.MarkerOffsetY, 
                currentTask.transform.position.z
                );
            marker.SetPosition(markerPos);
            marker.HintText.SetText(currentTask.LocalizationIndentificator);
        }
        else
        {
            MissionCompleted();
        }
    }

    public void TaskComplete()
    {
        currentTask.Deactivate();
        NextTask();
    }

    private void MissionCompleted()
    {
        completionState = true;
        ActivateOnMissionComplete();
        marker.gameObject.SetActive(false);
        levelManager.MissionCompleted(completionState);

        this.gameObject.SetActive(false);
    }

    private void MissionFailed()
    {
        completionState = false;
        ActivateOnMissionComplete();
        marker.gameObject.SetActive(false);
        levelManager.MissionCompleted(completionState);

        this.gameObject.SetActive(false);
    }

    private void DeactivateOnMissionStart()
	{
        foreach (GameObject obj in objectsToDeactivateOnMissionStart)
        {
            obj.SetActive(false);
        }
    }

    private void ActivateOnMissionComplete()
	{
        foreach (GameObject obj in objectsToActivateOnMissionEnd)
        {
            obj.SetActive(true);
        }
    }
}