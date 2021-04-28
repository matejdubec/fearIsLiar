using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMovingWall : MonoBehaviour
{
    [SerializeField] private float movingSpeed = .5f;
    [SerializeField] private float stoppingDistance = 1.5f;
    private CClosingInManager manager; 
    private Vector3 origin;
    private bool isMoving = false;

    Rigidbody rbody = null;


    public void Init(CClosingInManager _manager)
    {
        rbody = GetComponent<Rigidbody>();
        origin = transform.position;
        manager = _manager;
    }

    private void Update()
    {
        if(isMoving)
        {
            if (origin.y - stoppingDistance < transform.position.y)
            {
                transform.Translate(Vector3.up * movingSpeed * Time.deltaTime);
            }
            else
            {
                manager.MissionCompleted(false);
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
