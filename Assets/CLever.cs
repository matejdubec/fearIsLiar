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
            this.Emit();
            top.transform.localPosition = topOriginPosition;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Hand") || !isActive)
        {
            return;
        }

        if(top.ActiveHand && other.GetComponent<CustomHand>() == top.ActiveHand)
        {
            Vector3 lookAtPosition = new Vector3(controlledTransform.position.x, other.transform.position.y, other.transform.position.z);
            controlledTransform.LookAt(lookAtPosition);
            
            float current = controlledTransform.localEulerAngles.x;
            controlledTransform.localEulerAngles = new Vector3(current, 0f, 0f);

            if (current > 45f && current < 180f)
            {
                controlledTransform.localEulerAngles = new Vector3(45f, controlledTransform.localEulerAngles.y, controlledTransform.localEulerAngles.z);
                this.ChangeState(ELeverState.Down);
            }
            else if(current < 315 && current > 180f)
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
        material.SetFloat("_EmissiveExposureWeight", 1);
    }
}
