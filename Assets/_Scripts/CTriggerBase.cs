using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CTriggerBase : MonoBehaviour
{
	[SerializeField] protected Animator animator;
	[SerializeField] protected string audioString;
    [SerializeField] protected bool canRepeat = false;
    protected bool isActive = true;


	protected virtual void OnTriggerEnter(Collider other)
	{
		if (audioString.Length == 0 || !other.CompareTag("Player"))
			return;
	}

	protected virtual void PlayAnimation()
	{
		if (!animator)
			return;
	}

    protected virtual void PlaySound()
    {
        if (audioString.Length == 0)
            return;
    }
}
