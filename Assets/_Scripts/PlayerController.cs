using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;


public class PlayerController : MonoBehaviour
{
    public SteamVR_Action_Vector2 input;
    public float speed = 1;
    private CharacterController characterController;
    private float fallingVelocity, gravityForce = 9.81f;
    private float respawnBoundary = -45;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {      
        Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(input.axis.x, 0, input.axis.y));
        characterController.Move(speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up) - new Vector3(0, fallingVelocity, 0) * Time.deltaTime);

        if (!characterController.isGrounded)
		{
            fallingVelocity = fallingVelocity + (gravityForce * Time.deltaTime);
        }
		else
		{
            fallingVelocity = gravityForce;
		}

        if (Player.instance.hmdTransform.position.y < respawnBoundary)
            Player.instance.hmdTransform.position = new Vector3(0, 0, 0);
    }
}