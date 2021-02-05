using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMovingWall : MonoBehaviour
{
    [SerializeField] private float movingSpeed = 5f;
    [SerializeField] private float stoppingDistance = 10f;
    [SerializeField] private bool moveForward = true;
    private Vector3 origin;
    private bool isMoving = false;

    Rigidbody r = null;


    private void Start()
    {
        r = GetComponent<Rigidbody>();
        origin = transform.position;
    }

    private void Update()
    {
        if(isMoving)
        {
            if(moveForward)
            {
                if (origin.x + stoppingDistance > transform.position.x)
                {
                    transform.Translate(Vector3.forward * movingSpeed * Time.deltaTime);
                }                   
            }
            else
            {
                if (origin.x - stoppingDistance < transform.position.x)
                {
                    transform.Translate(Vector3.forward * -movingSpeed * Time.deltaTime);
                }
            }

        }
    }

    public void StartMoving()
    {
        isMoving = true;
    }

    public void StopMoving()
    {
        isMoving = false;
    }
}
