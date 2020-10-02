using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Hand : MonoBehaviour
{
    [SerializeField]
    private SteamVR_Action_Boolean m_GrabAction = null;
    public SteamVR_Action_Boolean GrabAction { get { return m_GrabAction; } private set { } }
    //private Socket m_socket = null;
    //public Socket Socket { get { return m_socket; }  private set { } }

    private SteamVR_Behaviour_Pose m_Pose = null;
    public SteamVR_Behaviour_Pose Pose { get { return m_Pose; } private set { } }
    private FixedJoint m_Joint = null;

    private Interactable m_CurrentInteractable = null;
    public Interactable CurrentInteractable { get { return m_CurrentInteractable; } set { m_CurrentInteractable = value; } }
    private List<Interactable> m_ContactInteractables = new List<Interactable>();

    private Slot slot;

    void Awake()
    {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
        m_Joint = GetComponent<FixedJoint>();
        //m_socket = GetComponent<Socket>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_GrabAction.GetStateDown(m_Pose.inputSource))
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

        if (m_GrabAction.GetStateUp(m_Pose.inputSource))
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

        m_ContactInteractables.Add(other.gameObject.GetComponent<Interactable>());

        slot = other.gameObject.GetComponent<Slot>();        
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Interactable"))
        {
            return;
        }

        m_ContactInteractables.Remove(other.gameObject.GetComponent<Interactable>());

        if (slot)
        {
            slot = null;
        }
    }

    public void PickUp()
    {
        m_CurrentInteractable = GetNearestInteractable();

        if (!m_CurrentInteractable)
        {
            return;
        }

        m_CurrentInteractable.transform.position = this.transform.position;

        Rigidbody targetBody = m_CurrentInteractable.GetComponent<Rigidbody>();
        m_Joint.connectedBody = targetBody;

        m_CurrentInteractable.m_Hand = this;
    }

    public void PickUp(Interactable interactable)
    {
        m_CurrentInteractable = interactable;

        if (m_CurrentInteractable)
        {
            //check if it is already held by any hand
            if (m_CurrentInteractable.m_Hand)
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
            m_CurrentInteractable.m_Hand = null;
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
