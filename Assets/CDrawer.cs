using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDrawer : MonoBehaviour
{

    [SerializeField] private CInteractable top;
    [SerializeField] private Transform controlledTransform;
    private Vector3 leverBaseForward;
    private Vector3 topOriginPosition;

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
        //top.transform.localPosition = topOriginPosition;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Hand"))
        {
            return;
        }

        if (top.ActiveHand && other.GetComponent<CCustomHand>() == top.ActiveHand)
        {

            top.transform.position = new Vector3(0, 0, other.transform.position.z);

            controlledTransform.position = Vector3.Lerp(controlledTransform.position, 
                new Vector3(controlledTransform.position.x, controlledTransform.position.y, top.transform.position.z - offset), 8 * Time.deltaTime);

            
            //// Zaklad co sme robili
            //Vector3 leverDirection = (top.transform.position - controlledTransform.position).normalized;
            //leverDirection.y = 0f;
            //leverDirection.Normalize();
            //Vector3 handDirection = other.transform.position - controlledTransform.position;

            //// Projekcia vektoru na rovinu v ktorej sa rotuje
            //Vector3 transformedPlaneNormal = Vector3.Dot(controlledTransform.forward, handDirection) * controlledTransform.forward;
            //Vector3 transformedHandDirection = handDirection - transformedPlaneNormal;

            ////controlledTransform.transform.position = top.


            //// Uhol so znamienkom
            //float deltaAngle = Vector3.SignedAngle(leverDirection, transformedHandDirection, controlledTransform.up);

            //// Rotacia okolo definovanej osy
            ////Vector3 transformedLeverDirection = Quaternion.AngleAxis(deltaAngle, controlledTransform.up) * controlledTransform.forward;
            //controlledTransform.Rotate(Vector3.up, deltaAngle);

            //// Ak je mimo 45 stupnov, neaktualizujem
            ///*if (Vector3.Angle(transformedLeverDirection, leverBaseForward) > 45f)
            //{
            //    controlledTransform.forward = transformedLeverDirection;
            //}*/

        }
    }
}
