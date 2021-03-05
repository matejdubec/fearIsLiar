using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CKey : MonoBehaviour
{
    private CMissionTaskKey taskKey;
    private Interactable interactable;
    private bool isActive = false;

    public void Init(CMissionTaskKey _taskKey)
    {
        taskKey = _taskKey;
        interactable = GetComponent<Interactable>();
    }

    private void Update()
    {
        if (isActive)
        {
            if (interactable.ActiveHand)
            {
                taskKey.KeyPickedUp(this);
            }
        }
    }

    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
        this.gameObject.SetActive(false);
    }
}
