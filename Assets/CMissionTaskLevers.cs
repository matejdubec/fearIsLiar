using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMissionTaskLevers : CMissionTaskBase
{
    [SerializeField] private List<CLever> levers;
    private int counter = 0;

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
                if (lever.hasCorrectState())
                {
                    counter++;
                }
                else
                {
                    counter = 0;
                }
            }

            if (counter == levers.Count)
            {
                foreach (CLever lever in levers)
                {
                    lever.Deactivate();
                }

                this.TaskCompleted();
            }
        }
    }
}
