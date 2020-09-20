using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Hand : MonoBehaviour
{
    [SerializeField]
    private SteamVR_Action_Boolean m_GrabAction = null;

    private SteamVR_Behaviour_Pose m_Pose = null;
    private FixedJoint m_Joint = null;

    private Interactable m_CurrentInteractable = null;
    private List<Interactable> m_ContactInteractables = new List<Interactable>();

    void Awake()
    {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
        m_Joint = GetComponent<FixedJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_GrabAction.GetStateDown(m_Pose.inputSource))
        {
            PickUp();
        }

        if (m_GrabAction.GetStateUp(m_Pose.inputSource))
        {
            Drop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Interactable"))
        {
            return;
        }

        m_ContactInteractables.Add(other.gameObject.GetComponent<Interactable>());
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Interactable"))
        {
            return;
        }

        m_ContactInteractables.Remove(other.gameObject.GetComponent<Interactable>());

    }

    public void PickUp()
    {
        m_CurrentInteractable = GetNearestInteractable();

        if (m_CurrentInteractable)
        {
            //check if it is already held by any hand
            if(m_CurrentInteractable.m_Hand)
            {
                m_CurrentInteractable.m_Hand.Drop();
            }

            m_CurrentInteractable.transform.position = this.transform.position;

            Rigidbody targetBody = m_CurrentInteractable.GetComponent<Rigidbody>();
            m_Joint.connectedBody = targetBody;

            m_CurrentInteractable.m_Hand = this;
        }

    }

    public void Drop()
    {
        if (m_CurrentInteractable)
        {
            //applu velocity
            Rigidbody targetBody = m_CurrentInteractable.GetComponent<Rigidbody>();
            targetBody.velocity = m_Pose.GetVelocity();
            targetBody.angularVelocity = m_Pose.GetAngularVelocity();

            //detach
            m_Joint.connectedBody = null;
            m_CurrentInteractable = null;
        }

    }

    private Interactable GetNearestInteractable()
    {
        Interactable nearest = null;
        float minDistance = float.MaxValue;
        float distance = 0.0f;

        foreach(Interactable interactable in m_ContactInteractables)
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
