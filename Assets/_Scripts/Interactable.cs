using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Interactable : MonoBehaviour
{
	public CustomHand m_Hand = null;

	private bool isAvailable = true;
	public bool IsAvailable { get { return isAvailable; }  set { isAvailable = value; } }

    public UnityEvent onHandHover;

    private void OnTriggerEnter(Collider other)
    {   
        if(other.gameObject.CompareTag("Player"))
            onHandHover.Invoke();
    }
}
