using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class CGetMissionItem : MonoBehaviour
{
    private Interactable interactable = null;
    [SerializeField] private CWaypoint waypoint;
    private CMissionManager missionManager;
    public CMissionManager MissionManager { set { missionManager = value; } }

    void Awake()
    {
        interactable = GetComponent<Interactable>();
    }

    void Start()
    {
        interactable.IsHeld += IsHeld;
    }

    private void IsHeld()
    {
        if(missionManager.CurrentWaypoint == waypoint)
        {
            waypoint.TaskCompleted();
        }
    }
}
