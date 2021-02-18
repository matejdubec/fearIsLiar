using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMissionTaskCubes : CMissionTaskBase
{
    [SerializeField] List<CCollideArea> collideAreas;
    [SerializeField] List<CColorfulCube> colorfulCubes;
    private int cubeCounter = 0;

    public override void Init(CMissionTaskManager _missionManager)
    {
        base.Init(_missionManager);

        foreach (CCollideArea area in collideAreas)
        {
            area.Init(this);
        }

        foreach (CColorfulCube cube in colorfulCubes)
        {
            cube.Init(this);
        }
    }

    public override void Activate()
    {
        base.Activate();

        foreach (CCollideArea area in collideAreas)
        {
            area.Activate();
        }

        foreach (CColorfulCube cube in colorfulCubes)
        {
            cube.Activate();
        }
    }

    public void CubeCollected()
    {
        if(isCurrent)
        {
            cubeCounter++;
            if (cubeCounter >= collideAreas.Count)
            {
                foreach (CCollideArea area in collideAreas)
                {
                    area.Deactivate();
                }

                this.TaskCompleted();
            }
        }
    }
}
