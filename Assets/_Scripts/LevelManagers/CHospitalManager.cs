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
            CGameManager.Instance.Player.ShowFlashlight(true, "right");
        }
    }

	public override void MissionCompleted(bool _missionCompletedSuccessfully, float _completionTime = 0.0f)
	{
        CGameManager.Instance.Player.ShowFlashlight(false, "right");
        base.MissionCompleted(_missionCompletedSuccessfully);      
    }
}
