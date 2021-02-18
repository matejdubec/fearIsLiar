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
            Vector3 lookAtPosition = new Vector3(controlledTransform.position.x, other.transform.position.y, other.transform.position.z);
            controlledTransform.LookAt(lookAtPosition);

            //Vector3 dir1 = (top.transform.position - this.controlledTransform.position).normalized;
            //Vector3 dir2 = (other.transform.position - this.controlledTransform.position);
            //dir2.x = 0f;
            //dir2.Normalize();
            //float currentAngle = Vector3.Angle(dir1, dir2);
            //transform.Rotate(transform.right, angle);
            //controlledTransform.forward = Quaternion.Euler(targetAngle * Time.deltaTime * 3f, 0f, 0f) * controlledTransform.right;
            //this.controlledTransform.forward = dir2;
            //transform.forward
            //transform.position = Quaternion.Euler(angle, 0.0f, 0.0f) * transform.position;

            float current = controlledTransform.localEulerAngles.x;
            controlledTransform.localEulerAngles = new Vector3(current, 0f, 0f);

            if (current > 45f && current < 180f)
            {
                controlledTransform.localEulerAngles = new Vector3(45f, controlledTransform.localEulerAngles.y, controlledTransform.localEulerAngles.z);
                this.ChangeState(ELeverState.Down);
            }
            else if (current < 315 && current > 180f)
            {
                controlledTransform.localEulerAngles = new Vector3(315f, controlledTransform.localEulerAngles.y, controlledTransform.localEulerAngles.z);
                this.ChangeState(ELeverState.Up);
            }
            else
            {
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
