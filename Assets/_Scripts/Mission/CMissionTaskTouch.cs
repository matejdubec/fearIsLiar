using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class CMissionTaskTouch : CMissionTaskBase
{
    [SerializeField] private Material materialToChangeOnComplete = null;
    private MeshRenderer meshRenderer;

    public override void Init(CMissionTaskManager _missionManager)
    {
        base.Init(_missionManager);

        if (materialToChangeOnComplete)
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }
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
        if (materialToChangeOnComplete)
        {
            meshRenderer.material = materialToChangeOnComplete;
        }

        base.TaskCompleted();
    }

    public override void Deactivate()
    {
        //base.Deactivate();
    }
}
