using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLever : MonoBehaviour
{
    [SerializeField] private Interactable top;
    private float forwardBackwardTilt = 0;
    Vector3 topOriginPosition;

    private void Start()
    {
        topOriginPosition = top.transform.localPosition;
    }

    private void Update()
    {
        top.transform.localPosition = topOriginPosition;
        forwardBackwardTilt = top.transform.rotation.eulerAngles.z;

        if (forwardBackwardTilt < 355 && forwardBackwardTilt > 290)
        {
            forwardBackwardTilt = Mathf.Abs(forwardBackwardTilt - 360);
        }
        else if (forwardBackwardTilt > 5 && forwardBackwardTilt < 74)
        {
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(!other.CompareTag("Hand"))
        {
            return;
        }

        if(top.ActiveHand && other.GetComponent<CustomHand>() == top.ActiveHand)
        {
            Vector3 lookAtPosition = new Vector3(transform.position.x, other.transform.position.y, other.transform.position.z);      
            transform.LookAt(lookAtPosition);
            if(forwardBackwardTilt > 5 && forwardBackwardTilt < 74)
            {
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 74);
            }

            if (forwardBackwardTilt < 355 && forwardBackwardTilt > 290)
            {
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 290);
            }
        }
    }
}
