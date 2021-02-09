using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLever : MonoBehaviour
{
    [SerializeField] private Transform top;

    [SerializeField] private float forwardBackwardTilt = 0;
    [SerializeField] private float sideTosideTilt = 0;

    private void Update()
    {
        forwardBackwardTilt = top.rotation.eulerAngles.z;

        if (forwardBackwardTilt < 355 && forwardBackwardTilt > 290)
        {
            forwardBackwardTilt = Mathf.Abs(forwardBackwardTilt - 360);
        }
        else if (forwardBackwardTilt > 5 && forwardBackwardTilt < 74)
        {
        }

        sideTosideTilt = top.rotation.eulerAngles.z;
        if(sideTosideTilt < 355 && sideTosideTilt > 290)
        {
            sideTosideTilt = Mathf.Abs(sideTosideTilt - 360);
        }
        else if (sideTosideTilt > 5 && sideTosideTilt < 74)
        {
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Hand"))
        {
            transform.LookAt(other.transform.position, transform.up);
            transform.eulerAngles = new Vector3(Mathf.Clamp(transform.rotation.eulerAngles.x, 210, 330), 0, 0);
        }
    }
}
