using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPushDoor : MonoBehaviour
{
    private Quaternion originRotation;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = Vector3.zero;
        rb.inertiaTensorRotation = Quaternion.identity;
        originRotation = transform.rotation;
    }

    private void Update()
    {
        if (rb.angularVelocity != Vector3.zero && transform.rotation == originRotation)
        {
            rb.angularVelocity = Vector3.zero;
        }
    }
}
