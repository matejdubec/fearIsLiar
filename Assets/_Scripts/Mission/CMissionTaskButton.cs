using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CMissionTaskButton : CMissionTaskBase
{
    [SerializeField] List<CVRButton> buttons;

    [SerializeField] private List<EColor> safeCode;
    private int safeCodeIndex = 0;

    public override void Init(CMissionTaskManager _missionManager)
    {
        base.Init(_missionManager);

        foreach (CVRButton button in buttons)
        {
            button.Init(this);
        }
    }

    public override void Activate()
    {
        base.Activate();

        foreach( CVRButton button in buttons)
        {
            button.Activate();
        }
    }

    public void ButtonPressed(CVRButton button)
    {
        if (button.ButtonColor == safeCode[safeCodeIndex])
        {
            safeCodeIndex++;
            if(safeCode.Count <= safeCodeIndex)
            {
                foreach (CVRButton b in buttons)
                {
                    b.Deactivate();
                }

                this.TaskCompleted();
            }
        }
        else
        {
            safeCodeIndex = 0;
        }
    }
}
