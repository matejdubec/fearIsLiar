using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBasementManager : CLevelManager
{
    private CSpiderPooler spiderPooler;

    public override void Init()
    {
        base.Init();
        spiderPooler = GetComponent<CSpiderPooler>();
        spiderPooler.Init();
        if (activeMission)
        {
            activeMission.StartMission();
        }
    }
}
