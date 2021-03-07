using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CKey : MonoBehaviour
{
    private CMissionTaskKey taskKey;
    private CInteractable interactable;
    private bool isActive = false;

    public void Init(CMissionTaskKey _taskKey)
    {
        taskKey = _taskKey;
        interactable = GetComponent<CInteractable>();
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
