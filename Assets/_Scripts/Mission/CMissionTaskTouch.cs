using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class CMissionTaskTouch : CMissionTaskBase
{
    public override void Init(CMissionTaskManager _missionManager)
    {
        base.Init(_missionManager);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hand"))
        {
            if (isCurrent)
            {
                this.TaskCompleted();
            }
        }
    }

    protected override void TaskCompleted()
    {
        base.TaskCompleted();
    }

    public override void Deactivate()
    {
        base.Deactivate();
        this.transform.parent.gameObject.SetActive(false);
    }
}
