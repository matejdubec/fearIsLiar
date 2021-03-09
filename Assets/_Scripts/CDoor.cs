using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDoor : MonoBehaviour
{

    [SerializeField] private CInteractable top;
    [SerializeField] private Transform controlledTransform;
    private Vector3 leverBaseForward;
    private Vector3 topOriginPosition;


    private void Awake()
    {
        this.leverBaseForward = (top.transform.position - controlledTransform.position).normalized;
        topOriginPosition = top.transform.localPosition;
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
            // Zaklad co sme robili
            Vector3 leverDirection = (top.transform.position - controlledTransform.position).normalized;
            leverDirection.y = 0f;
            leverDirection.Normalize();
            Vector3 handDirection = other.transform.position - controlledTransform.position;

            // Projekcia vektoru na rovinu v ktorej sa rotuje
            Vector3 transformedPlaneNormal = Vector3.Dot(controlledTransform.up, handDirection) * controlledTransform.up;
            Vector3 transformedHandDirection = handDirection - transformedPlaneNormal;

            // Uhol so znamienkom
            float deltaAngle = Vector3.SignedAngle(leverDirection, transformedHandDirection, controlledTransform.up);

            // Rotacia okolo definovanej osy
            //Vector3 transformedLeverDirection = Quaternion.AngleAxis(deltaAngle, controlledTransform.up) * controlledTransform.forward;
            controlledTransform.Rotate(Vector3.up, deltaAngle);

            // Ak je mimo 45 stupnov, neaktualizujem
            /*if (Vector3.Angle(transformedLeverDirection, leverBaseForward) > 45f)
            {
                controlledTransform.forward = transformedLeverDirection;
            }*/
        }
    }
}
