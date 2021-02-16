using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMissionTaskCubes : CMissionTaskBase
{
    [SerializeField] List<CCollideArea> collideAreas;
    private int cubeCounter = 0;

    public override void Init(CMissionTaskManager _missionManager)
    {
        base.Init(_missionManager);

        foreach (CCollideArea area in collideAreas)
        {
            area.Init(this);
        }
    }

    public void CubeCollected()
    {
        cubeCounter++;
        if(cubeCounter >= collideAreas.Count)
        {
            this.TaskCompleted();
        }
    }
}
