using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CClosingInManager : CLevelManager
{
    [SerializeField] private List<CMovingWall> movingWalls;
    [SerializeField] private float wallsDelay = 5f;

    public override void Init()
    {
        base.Init();

        foreach (CMovingWall wall in movingWalls)
        {
            wall.Init(this);
        }

        if (activeMission)
        {
            activeMission.StartMission();
            StartCoroutine("StartWalls");
        }
    }

    private IEnumerator StartWalls()
    {
        yield return new WaitForSeconds(wallsDelay);

        foreach (CMovingWall wall in movingWalls)
        {
            wall.StartMoving();
        }
    }

    public override void MissionCompleted(bool _missionCompletitionState)
    {
        foreach (CMovingWall wall in movingWalls)
        {
            wall.StopMoving();
        }

        base.MissionCompleted(_missionCompletitionState);
    }
}
