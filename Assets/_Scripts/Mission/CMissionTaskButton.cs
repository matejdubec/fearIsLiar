using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CMissionTaskButton : CMissionTaskBase
{
    [SerializeField] List<CVRButton> buttons;
    private int buttonsCompletedCounter = 0;

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

    public void ButtonCompleted(CVRButton _button)
    {
        CVRButton button = buttons.Find(x => x == _button);
        button.Deactivate();
        buttonsCompletedCounter++;

        if(buttonsCompletedCounter == buttons.Count)
        {
            this.TaskCompleted();
        }
    }
}
