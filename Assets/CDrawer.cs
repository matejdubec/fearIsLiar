using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDrawer : MonoBehaviour
{

    [SerializeField] private CInteractable top;
    [SerializeField] private float maxOpenRange = 0.3f;
    private Vector3 topOriginPosition;
    private Vector3 prevHandPosition = Vector3.zero;

    private float controlledTransformOriginXPosition;
    private float controlledTransformEndXPosition = 0.0f;

    private void Awake()
    {
        topOriginPosition = top.transform.localPosition;
        controlledTransformOriginXPosition = transform.localPosition.x;
        controlledTransformEndXPosition = controlledTransformOriginXPosition + maxOpenRange;
    }

    private void Update()
    {
        top.transform.localPosition = topOriginPosition;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Hand"))
        {
            return;
        }

        if (top.ActiveHand && other.GetComponent<CCustomHand>() == top.ActiveHand)
        {
            if (prevHandPosition != Vector3.zero)
            {
                float diff = transform.InverseTransformDirection(other.transform.localPosition - prevHandPosition).x;
                float shift = transform.localPosition.x + diff;
                if (controlledTransformOriginXPosition < shift && shift < controlledTransformEndXPosition)
                {
                    transform.Translate(Vector3.right * diff);
                }
            }
            prevHandPosition =  other.transform.localPosition;
        }
    }
}
