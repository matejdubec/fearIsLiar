using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDrawer : MonoBehaviour
{

    [SerializeField] private CInteractable top;
    [SerializeField] private Transform controlledTransform;
    private Vector3 leverBaseForward;
    private Vector3 topOriginPosition;
    private Vector3 prevHandPosition = Vector3.zero;

    private float offset = 0.0f;
    private Vector3 controlledTransformOriginPosition;
    [SerializeField ]private Transform controlledTransformEndPosition;

    private void Awake()
    {
        this.leverBaseForward = (top.transform.position - controlledTransform.position).normalized;
        topOriginPosition = top.transform.localPosition;
        offset = Vector3.Distance(top.transform.position, controlledTransform.position);
        controlledTransformOriginPosition = controlledTransform.position;
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
                //float diff = prevHandPosition.x - other.transform.position.x;
                //float diff2 = controlledTransform.localPosition.x - diff;
                //if (diff2 > controlledTransformOriginPosition.x && diff2 < controlledTransformEndPosition.localPosition.x)
                //{
                //    //controlledTransform.position = new Vector3(controlledTransform.position.x, controlledTransform.position.y, diff2);
                //    controlledTransform.Translate(new Vector3(diff2, 0, 0) * (8 * Time.deltaTime));
                //}

                float diff = prevHandPosition.z - other.transform.position.z;
                float diff2 = controlledTransform.position.z - diff;
                if (diff2 > controlledTransformOriginPosition.z && diff2 < controlledTransformEndPosition.position.z)
                {
                    controlledTransform.position = new Vector3(controlledTransform.position.x, controlledTransform.position.y, diff2);
                   
                }
            }
            prevHandPosition =  other.transform.position;
        }
    }
}
