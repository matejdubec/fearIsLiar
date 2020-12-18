using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCollideMissionObject : MonoBehaviour
{
    [SerializeField] private CWaypoint waypoint;
    private CMissionManager missionManager;
    public CMissionManager MissionManager { set { missionManager = value; } }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("MissionObject"))
        {
            if (missionManager.CurrentWaypoint == waypoint)
            {
                waypoint.TaskCompleted();
            }
        }
    }
}
