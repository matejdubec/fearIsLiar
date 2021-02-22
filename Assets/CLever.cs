using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ELeverState
{
    Up,
    Down,
    Between,
}


public class CLever : CEmitable
{
    [SerializeField] private Interactable top;
    [SerializeField] private Transform controlledTransform;
    [SerializeField] private ELeverState leverDesiredState;
    private ELeverState leverCurrentState;
    private bool isActive = false;
    private CMissionTaskLevers taskLevers;
    private float targetAngle = 0f;

    Vector3 topOriginPosition;

    private Vector3 leverBaseForward;

    private void Awake()
    {
        this.leverBaseForward = (top.transform.position - controlledTransform.position).normalized;
    }

    public void Init(CMissionTaskLevers _missionTaskLevers)
    {
        base.Init(top.GetComponent<MeshRenderer>().material);
        topOriginPosition = top.transform.localPosition;      
        leverCurrentState = ELeverState.Between;
        isActive = true;
        taskLevers = _missionTaskLevers;
    }

    private void Update()
    {
        if(isActive)
        {
            this.StartBlinking();
            top.transform.localPosition = topOriginPosition;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Hand") || !isActive)
        {
            return;
        }

        if (top.ActiveHand && other.GetComponent<CustomHand>() == top.ActiveHand)
        {
            // Zaklad co sme robili
            Vector3 leverDirection = (top.transform.position - controlledTransform.position).normalized;
            Vector3 handDirection = other.transform.position - controlledTransform.position;

            // Projekcia vektoru na rovinu v ktorej sa rotuje
            Vector3 transformedPlaneNormal = Vector3.Dot(controlledTransform.right, handDirection) * controlledTransform.right;
            Vector3 transformedHandDirection = handDirection - transformedPlaneNormal;

            // Uhol so znamienkom
            float deltaAngle = Vector3.SignedAngle(leverDirection, transformedHandDirection, controlledTransform.right);

            // Rotacia okolo definovanej osy
            Vector3 transformedLeverDirection = Quaternion.AngleAxis(deltaAngle, controlledTransform.right) * controlledTransform.forward;

            // Ak je mimo 45 stupnov, neaktualizujem
            if (Vector3.SignedAngle(transformedLeverDirection, leverBaseForward, controlledTransform.right) > 45f)
            {
                this.ChangeState(ELeverState.Up);
            }
            else if (Vector3.SignedAngle(transformedLeverDirection, leverBaseForward, controlledTransform.right) < -45f)
            {
                this.ChangeState(ELeverState.Down);
            }
            else
            {             
                controlledTransform.forward = transformedLeverDirection;
                this.ChangeState(ELeverState.Between);
            }        
        }
    }

    private void ChangeState(ELeverState _state)
    {
        if(_state != leverCurrentState)
        {
            leverCurrentState = _state;
            taskLevers.LeverStateChanged();
        }
    }

    public bool hasCorrectState()
    {
        return leverCurrentState == leverDesiredState;
    }

    public void Deactivate()
    {
        isActive = false;
        this.Emit();
    }
}
