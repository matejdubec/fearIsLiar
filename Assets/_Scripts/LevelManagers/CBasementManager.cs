using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBasementManager : CLevelManager
{
    public override void Init()
    {
        base.Init();
        if (activeMission)
        {
            activeMission.StartMission();
        }
    }
}
