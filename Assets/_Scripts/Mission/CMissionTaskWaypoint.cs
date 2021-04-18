using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CMissionTaskWaypoint : CMissionTaskBase
{
    private float distance = 2.1f;

    private void Update()
    {        
        if(isCurrent & CGameManager.Instance.Player)
        {
            if (Vector3.Distance(CGameManager.Instance.Player.GetPlayerPosition(), transform.position) < distance)
            {
                this.TaskCompleted();
                this.Deactivate();
            }
        }
    }
}
