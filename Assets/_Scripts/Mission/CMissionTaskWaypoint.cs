using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CMissionTaskWaypoint : CMissionTaskBase
{
    private float distance = 0.5f;

    private void Update()
    {        
        if(isCurrent)
        {
            if (Vector3.Distance(SteamVR_Render.Top().origin.position, transform.position) < distance)
            {
                this.TaskCompleted();
                this.Deactivate();
            }
        }
    }
}
