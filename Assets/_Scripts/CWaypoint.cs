using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EWaypointType
{
    Basic,
    SimpleTask,
}


public class CWaypoint : MonoBehaviour
{
    [SerializeField] private EWaypointType taskType;
    [SerializeField] private string localizationIndentificator = null;
    public string LocalizationIndentificator { get { return localizationIndentificator; } }

    private CMissionManager missionManager;

    public void Init(CMissionManager myMissionManager) 
    {
        this.missionManager = myMissionManager;
        this.gameObject.SetActive(false);
    }

    public void Activate()
    {
        this.gameObject.SetActive(true);
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

    }
}