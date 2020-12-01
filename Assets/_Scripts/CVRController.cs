using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CVRController : MonoBehaviour
{
    private float gravity = 9.81f;
    private float fallingVelocity = 0.0f;
    public float accelaration = 0.1f;
    public float maxSpeed = 1.0f;

    [SerializeField]
    private Animator animator;
    [SerializeField] private Canvas hintCanvas;

    [SerializeField] private Canvas returnToMenuCanvas;

    public SteamVR_Action_Boolean movePress = null;
    public SteamVR_Action_Boolean GrabGripPress = null;
    public SteamVR_Action_Boolean leftTrackpadButtonPress = null;
    public SteamVR_Action_Boolean rightTrackpadButtonPress = null;
    public SteamVR_Action_Vector2 touchPad = null;

    [SerializeField] private float snapAngle = 45f;

    private float currentSpeed = 0.0f;

    private CharacterController characterController = null;
    private Transform cameraRig = null;
    private Transform head = null;   

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

	// Start is called before the first frame update
	void Start()
    {
        cameraRig = SteamVR_Render.Top().origin;
        head = SteamVR_Render.Top().head;
    }

    // Update is called once per frame
    void Update()
    {
        HandleHeight();
        CalculateMovement();
        SnapTurn();
        HandleGrabGrip();
    }

    private void HandleHeight()
    {
        //get the head in local space
        float headHeight = Mathf.Clamp(head.localPosition.y, 1, 2);
        characterController.height = headHeight;

        //cut in half
        Vector3 newCenter = Vector3.zero;
        newCenter.y = characterController.height / 2;
        newCenter.y += characterController.skinWidth;

        //move capsule in local space
        newCenter.x = head.localPosition.x;
        newCenter.z = head.localPosition.z;

        //rotate
        newCenter = Quaternion.Euler(0, -transform.eulerAngles.y, 0) * newCenter;
        

        //apply
        characterController.center = newCenter;
    }

    private void CalculateMovement()
    {
        //movement orientation
        Vector3 orientationEuler = new Vector3(0, head.eulerAngles.y, 0);
        Quaternion orientation = Quaternion.Euler(orientationEuler);
        Vector3 movement = Vector3.zero;

        //if not moving
        if (movePress.GetStateUp(SteamVR_Input_Sources.RightHand))
        {
            currentSpeed = 0;
            animator.SetBool("isWalking", false);
        }          

        // if button pressed
        if(movePress.state)
		{
            //Add clamp
            currentSpeed += accelaration;
            currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);

            //orientation
            movement += orientation * (currentSpeed * Vector3.forward);

            animator.SetBool("isWalking", true);
		}

        //gravity
        if (characterController.isGrounded)
            fallingVelocity = 0.0f;  
        else
            fallingVelocity += gravity * Time.deltaTime;

        movement.y -= fallingVelocity;

        //apply
        characterController.Move(movement * Time.deltaTime);
	}

    private void SnapTurn()
	{
		if (leftTrackpadButtonPress.GetStateDown(SteamVR_Input_Sources.LeftHand) &&  touchPad.GetAxis(SteamVR_Input_Sources.LeftHand).x < -0.9f)
		{
            transform.Rotate(Vector3.up, -snapAngle);
        }
        else if (rightTrackpadButtonPress.GetStateDown(SteamVR_Input_Sources.LeftHand) && touchPad.GetAxis(SteamVR_Input_Sources.LeftHand).x > 0.9f)
		{
            transform.Rotate(Vector3.up, snapAngle);
        }
	}

    private void HandleGrabGrip()
	{
        if (GrabGripPress.GetStateDown(SteamVR_Input_Sources.LeftHand))
        {
            bool isVisible = hintCanvas.gameObject.activeSelf;
            hintCanvas.gameObject.SetActive(!isVisible);
        }

        if (GrabGripPress.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            bool isVisible = returnToMenuCanvas.gameObject.activeSelf;
            returnToMenuCanvas.gameObject.SetActive(!isVisible);
        }
    }

	private void OnDestroy()
	{
		//TODO mozno urobi input cez eventy ako v CHandAnimator
	}

    public void SetPosition(Vector3 newPosition)
	{
        this.transform.position = newPosition;
    }
}
