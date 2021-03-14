using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRoomManager : CLevelManager
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
