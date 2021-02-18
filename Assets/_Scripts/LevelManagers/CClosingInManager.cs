using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CClosingInManager : CLevelManager
{
    [SerializeField] private List<CMovingWall> movingWalls;
    [SerializeField] private float wallsDelay = 5f;
    private CAudioManager audioManager;

    public override void Init()
    {
        base.Init();

        audioManager = GetComponent<CAudioManager>();
        audioManager.Init();

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

        audioManager.PlaySound("MovingWall");
    }

    public override void MissionCompleted(bool _missionCompletitionState)
    {
        foreach (CMovingWall wall in movingWalls)
        {
            wall.StopMoving();
        }

        audioManager.StopSound("MovingWall");

        base.MissionCompleted(_missionCompletitionState);
    }
}
