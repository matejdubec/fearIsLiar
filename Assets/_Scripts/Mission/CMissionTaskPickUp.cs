using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class CMissionTaskPickUp : CMissionTaskBase
{
    private Interactable interactable = null;

    public override void Init(CMissionTaskManager _missionManager)
    {
        base.Init(_missionManager);
        interactable = GetComponent<Interactable>();
    }

    private void Update()
    {
        if (isCurrent)
        {
            if (interactable.ActiveHand)
            {
                this.TaskCompleted();
            }
        }
    }

    public override void Deactivate()
    {
        //base.Deactivate();
    }
}
