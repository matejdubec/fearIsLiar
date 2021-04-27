using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COrbit : MonoBehaviour
{
    [SerializeField] private Transform target;


    void Update()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(dir);

        transform.localRotation = Quaternion.Slerp(transform.localRotation, rotation, Time.deltaTime);
        transform.Translate(0, 0, 6 * Time.deltaTime);
    }
}
