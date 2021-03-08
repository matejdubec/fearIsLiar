using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTriggerBase : MonoBehaviour
{
	[SerializeField] private Animator animator;
	[SerializeField] private string audioString;

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
}
