using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CHospitalManager : CLevelManager
{
    public override void Init()
    {
        base.Init();
        if(activeMission)
        {
            activeMission.StartMission();
        }
    }
}
