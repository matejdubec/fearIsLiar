using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class CustomHandExperimental : Hand
{
    private FixedJoint m_Joint = null;

    private SlotExperimental slot;

    protected override void Awake()
    {
        base.Awake();

        m_Joint = GetComponent<FixedJoint>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (grabGripAction.GetStateDown(trackedObject.inputSource))
        {
            if (slot)
            {
                slot.ReleaseObject(this);
            }
            else
            {
                AttachObject(slot.StoredObject.gameObject, GrabTypes.Grip);
            }  
        }

        if (grabGripAction.GetStateUp(trackedObject.inputSource))
        {
            if (slot)
            {
                slot.AttachObject(this);
            }
            else
            {
                DetachObject(this.currentAttachedObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        slot = other.gameObject.GetComponent<SlotExperimental>();        
    }

    private void OnTriggerExit(Collider other)
    {
        if (slot)
        {
            slot = null;
        }
    }
}
