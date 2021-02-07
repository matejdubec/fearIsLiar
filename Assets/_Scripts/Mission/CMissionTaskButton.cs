using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CMissionTaskButton : CMissionTaskBase
{
    [SerializeField] private int PressesToComplete = 5;
    private int pressCount = 0;

    private float pressLength = 0.5f;
    private bool pressed;
    private Vector3 origin;
    private Rigidbody rb;

    public override void Init(CMissionTaskManager _missionManager)
    {
        base.Init(_missionManager);
        origin = transform.localPosition;
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    private void Update()
    {
        if (isCurrent)
        {
            if(rb.constraints == RigidbodyConstraints.FreezeAll)
            {
                rb.constraints = RigidbodyConstraints.FreezeRotation;
            }

            this.CountPresses();

            if (pressCount >= PressesToComplete)
            {
                rb.constraints = RigidbodyConstraints.FreezeAll;
                this.TaskCompleted();
            }
        }
    }

    private void CountPresses()
    {
        float distance = Mathf.Abs(transform.localPosition.y - origin.y);
        if (distance >= pressLength)
        {
            transform.localPosition = new Vector3(origin.x, origin.y - pressLength, origin.z);
            if (!pressed)
            {
                pressed = true;
                pressCount++;
            }
        }
        else
        {
            pressed = false;
            transform.localPosition = new Vector3(origin.x, transform.localPosition.y, origin.z);
        }

        if (transform.localPosition.y > origin.y)
        {
            transform.localPosition = new Vector3(origin.x, origin.y, origin.z);
        }
    }
}
