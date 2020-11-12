﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CustomHand : MonoBehaviour
{
    [SerializeField]
    private SteamVR_Action_Boolean grabAction = null;

    private SteamVR_Behaviour_Pose pose = null;
    private FixedJoint joint = null;

	public Interactable CurrentInteractable { get; set; } = null;
	private List<Interactable> contactInteractables = new List<Interactable>();

    private Slot slot;

    void Awake()
    {
        pose = GetComponent<SteamVR_Behaviour_Pose>();
        joint = GetComponent<FixedJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (grabAction.GetStateDown(pose.inputSource))
        {
            if (slot)
            {
                slot.ReleaseObject(this);
            }
            else
            {
                PickUp();
            }  
        }

        if (grabAction.GetStateUp(pose.inputSource))
        {
            if (slot)
            {
                slot.AttachObject(this);
            }
            else
            {
                Drop();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Interactable"))
        {
            return;
        }

        contactInteractables.Add(other.gameObject.GetComponent<Interactable>());

        slot = other.gameObject.GetComponent<Slot>();        
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Interactable"))
        {
            return;
        }

        contactInteractables.Remove(other.gameObject.GetComponent<Interactable>());

        if (slot)
        {
            slot = null;
        }
    }

    public void PickUp()
    {
        CurrentInteractable = GetNearestInteractable();

        if (!CurrentInteractable)
        {
            return;
        }

        CurrentInteractable.transform.position = this.transform.position;

        Rigidbody targetBody = CurrentInteractable.GetComponent<Rigidbody>();
        joint.connectedBody = targetBody;

        CurrentInteractable.m_Hand = this;
    }

    public void PickUp(Interactable interactable)
    {
        CurrentInteractable = interactable;

        if (CurrentInteractable)
        {
            //check if it is already held by any hand
            if (CurrentInteractable.m_Hand)
            {
                CurrentInteractable.m_Hand.Drop();
            }

            CurrentInteractable.transform.position = this.transform.position;

            Rigidbody targetBody = CurrentInteractable.GetComponent<Rigidbody>();
            joint.connectedBody = targetBody;

            CurrentInteractable.m_Hand = this;
        }
    }

    public void Drop()
    {
        if (CurrentInteractable)
        {
            //applu velocity
            Rigidbody targetBody = CurrentInteractable.GetComponent<Rigidbody>();
            targetBody.velocity = pose.GetVelocity();
            targetBody.angularVelocity = pose.GetAngularVelocity();

            //detach
            joint.connectedBody = null;
            CurrentInteractable.m_Hand = null;
            CurrentInteractable = null;

        }
    }

    private Interactable GetNearestInteractable()
    {
        Interactable nearest = null;
        float minDistance = float.MaxValue;
        float distance = 0.0f;

        foreach(Interactable interactable in contactInteractables)
        {
            distance = (interactable.transform.position - this.transform.position).sqrMagnitude;

            if(distance < minDistance)
            {
                minDistance = distance;
                nearest = interactable;
            }
        }

        return nearest;
    }
}
