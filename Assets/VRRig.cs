using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VRMap
{
    public Transform vrTarget;
    public Transform rigTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;

    public void Map()
	{
        rigTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
	}
}

//https://www.youtube.com/watch?v=tBYl-aSxUe0

public class VRRig : MonoBehaviour
{
    [SerializeField]
    private VRMap head;
    [SerializeField]
    private VRMap lefthand;
    [SerializeField]
    private VRMap righthand;

    [SerializeField]
    private Transform headConstraint;
    private Vector3 headBodyOffset;
    [SerializeField]
    private float turnSmoothness;

    // Start is called before the first frame update
    void Start()
    {
        headBodyOffset = transform.position - headConstraint.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = headConstraint.position + headBodyOffset;
        transform.forward = Vector3.Lerp(transform.forward,
            Vector3.ProjectOnPlane(headConstraint.up, Vector3.up).normalized, Time.deltaTime * turnSmoothness);

        head.Map();
        righthand.Map();
        lefthand.Map();
    }
}
