using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ETaskType
{
    Waypoint,
    SimpleTask,
}


public class CTask : MonoBehaviour
{
    [SerializeField] private ETaskType taskType;
    [SerializeField] private GameObject waypointPrefab;
    [SerializeField] private string localizationIndentificator = null;

    private bool isFinished = false;

    public EventHandler TaskDone;

    public void Init()
    {
        this.gameObject.SetActive(false);
    }

    public void Activate()
    {
        this.gameObject.SetActive(true);
        CGameMaster.Instance.BackToMenuCanvasController.SetMainText(localizationIndentificator);

        if (taskType == ETaskType.Waypoint)
        {
            //TODO prerobit na pool
            Instantiate(waypointPrefab, this.transform.position, Quaternion.identity, this.transform);
        }
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        TaskCompleted();
    }

    public void TaskCompleted()
    {
        TaskDone.Invoke(this, null);
    }
}