using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class CMissionTaskCollide : CMissionTaskBase
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("MissionObject"))
        {
            if(isCurrent)
            {
                this.TaskCompleted();
            }
        }
    }
}
