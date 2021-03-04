using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMissionTaskLevers : CMissionTaskBase
{
    [SerializeField] private List<CLever> levers;

    public override void Init(CMissionTaskManager _missionManager)
    {
        base.Init(_missionManager);

        foreach (CLever lever in levers)
        {
            lever.Init(this);
        }
    }

    public void LeverStateChanged()
    {
        if (isCurrent)
        {
            foreach (CLever lever in levers)
            {
                if (!lever.hasCorrectState())
                {
                    return;
                }
            }

            foreach (CLever lever in levers)
            {
                lever.Deactivate();
            }

            this.TaskCompleted();
        }
    }
}
