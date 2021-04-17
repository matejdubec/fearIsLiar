using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.SceneManagement;

public class CVRController : MonoBehaviour
{
    private float gravity = 9.81f;
    private float fallingVelocity = 0.0f;
    public float accelaration = 0.1f;
    public float maxSpeed = 2.0f;

    [SerializeField] private Animator animator;
    [SerializeField] private CBackToMenuCanvasController returnToMenuCanvas;
    [SerializeField] private CPointer pointer;
    public CPointer Pointer { get { return pointer; } }

    [SerializeField] private SteamVR_Action_Boolean movePress = null;
    [SerializeField] private SteamVR_Action_Boolean GrabGripPress = null;
    [SerializeField] private SteamVR_Action_Boolean leftTrackpadButtonPress = null;
    [SerializeField] private SteamVR_Action_Boolean rightTrackpadButtonPress = null;
    [SerializeField] private SteamVR_Action_Vector2 touchPad = null;

    [SerializeField] private float snapAngle = 45f;

    [SerializeField] private CCustomHand rightHand, leftHand = null;
    [SerializeField] private GameObject flashlightPrefab = null;
    private GameObject flashlight = null;

    private float currentSpeed = 0.0f;

    private CharacterController characterController = null;
    private Transform head = null;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

	// Start is called before the first frame update
	void Start()
    {
        Refresh();
        pointer.Init();
    }

    public void Refresh()
    {
        head = SteamVR_Render.Top().head;
        pointer.Refresh();
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
        //ked to je zakomentovane tak mi funguje snap turn
        //po snap turn character controller ostane na kamere
        //newCenter = Quaternion.Euler(0, -transform.eulerAngles.y, 0) * newCenter;

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
            CGameManager.Instance.AudioManager.StopSound("PlayerRun");
        }          

        // if button pressed
        if(movePress.state)
		{
            if(!CGameManager.Instance.AudioManager.IsPlaying("PlayerRun"))
            {
                CGameManager.Instance.AudioManager.PlaySound("PlayerRun");
            }

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

        if (movePress.state & touchPad.GetAxis(SteamVR_Input_Sources.RightHand).y > 0.85f)
        {
            //apply
            characterController.Move(movement * Time.deltaTime);
        }
        else if (movePress.state & touchPad.GetAxis(SteamVR_Input_Sources.RightHand).y < -0.85f)
        {
            movement = new Vector3(-movement.x, movement.y, -movement.z);
            //apply
            characterController.Move(movement * Time.deltaTime);
        }
    }

    private void SnapTurn()
	{
		if (leftTrackpadButtonPress.GetStateDown(SteamVR_Input_Sources.LeftHand) &&  touchPad.GetAxis(SteamVR_Input_Sources.LeftHand).x < -0.9f)
		{
            transform.RotateAround(new Vector3(head.transform.position.x, 0, SteamVR_Render.Top().head.transform.position.z), Vector3.up, -snapAngle);
            //characterController.center.Set(head.localPosition.x, 0, head.localPosition.z);
        }
        else if (rightTrackpadButtonPress.GetStateDown(SteamVR_Input_Sources.LeftHand) && touchPad.GetAxis(SteamVR_Input_Sources.LeftHand).x > 0.9f)
		{
            transform.RotateAround(new Vector3(head.transform.position.x, 0, SteamVR_Render.Top().head.transform.position.z), Vector3.up, snapAngle);
            //characterController.center.Set(head.localPosition.x, 0, head.localPosition.z);
        }
    }

    private void HandleGrabGrip()
	{
        if (GrabGripPress.GetStateDown(SteamVR_Input_Sources.RightHand) && SceneManager.GetActiveScene().name != ESceneId.MainMenu.ToString())
        {
            bool isVisible = returnToMenuCanvas.gameObject.activeSelf;
            returnToMenuCanvas.Activate(!isVisible);
            pointer.Activate(!isVisible);
        }
    }

    public void SetPosition(Vector3 newPosition)
	{
        this.transform.position = newPosition;
    }

    public void ShowFlashlight()
	{
        if(!flashlight)
		{
            flashlight = Instantiate(flashlightPrefab);
        }

        rightHand.ShowFlashlight(flashlight);
    }

    public void HideFlashlight()
    {
        if (flashlight)
        {
            rightHand.HideFlashlight();
        }      
    }
}