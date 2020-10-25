using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CVRController : MonoBehaviour
{
    private float m_Gravity = 9.81f;
    private float m_FallingVelocity = 0.0f;
    public float m_Sensitivity = 0.1f;
    public float m_MaxSpeed = 1.0f;

    [SerializeField]
    private int heightForRespawn = -50;
    [SerializeField]
    private Animator animator;
    [SerializeField] private Canvas hintCanvas;

    public SteamVR_Action_Boolean m_MovePress = null;
    public SteamVR_Action_Vector2 m_MoveValue = null;
    public SteamVR_Action_Boolean m_HintPress = null;

    private float m_Speed = 0.0f;

    private CharacterController m_CharacterController = null;
    private Transform m_CameraRig = null;
    private Transform m_Head = null;

    private Vector3 originPosition;

    private void Awake()
    {
        m_CharacterController = GetComponent<CharacterController>();
        originPosition = this.transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_CameraRig = SteamVR_Render.Top().origin;
        m_Head = SteamVR_Render.Top().head;
    }

    // Update is called once per frame
    void Update()
    {
        HandleHeight();
        CalculateMovement();

		if (m_HintPress.GetStateDown(SteamVR_Input_Sources.LeftHand))
		{
            bool isVisible = hintCanvas.gameObject.activeSelf;
            hintCanvas.gameObject.SetActive(!isVisible);
		}

        if (this.transform.position.y <= heightForRespawn)
        {
            this.transform.position = originPosition;
        }
    }

    private void HandleHeight()
    {
        //get the head in local space
        float headHeight = Mathf.Clamp(m_Head.localPosition.y, 1, 2);
        m_CharacterController.height = headHeight;

        //cut in half
        Vector3 newCenter = Vector3.zero;
        newCenter.y = m_CharacterController.height / 2;
        newCenter.y += m_CharacterController.skinWidth;

        //move capsule in local space
        newCenter.x = m_Head.localPosition.x;
        newCenter.z = m_Head.localPosition.z;

        //rotate
        newCenter = Quaternion.Euler(0, -transform.eulerAngles.y, 0) * newCenter;

        //apply
        m_CharacterController.center = newCenter;
    }

    private void CalculateMovement()
    {
        //movement orientation
        Vector3 orientationEuler = new Vector3(0, m_Head.eulerAngles.y, 0);
        Quaternion orientation = Quaternion.Euler(orientationEuler);
        Vector3 movement = Vector3.zero;

        //if not moving
        if (m_MovePress.GetStateUp(SteamVR_Input_Sources.RightHand))
        {
            m_Speed = 0;
            animator.SetBool("isWalking", false);
        }          

        // if button pressed
        if(m_MovePress.state)
		{
            //Add clamp
            m_Speed += m_MoveValue.axis.y * m_Sensitivity;
            m_Speed = Mathf.Clamp(m_Speed, -m_MaxSpeed, m_MaxSpeed);

            //orientation
            movement += orientation * (m_Speed * Vector3.forward);

            animator.SetBool("isWalking", true);
		}

        //gravity
        if (m_CharacterController.isGrounded)
            m_FallingVelocity = 0.0f;  
        else
            m_FallingVelocity += m_Gravity * Time.deltaTime;

        movement.y -= m_FallingVelocity;

        //apply
        m_CharacterController.Move(movement * Time.deltaTime);
	}
}
