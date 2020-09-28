using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Interactable : MonoBehaviour
{
	public Hand m_Hand = null;
	private Socket activeSocket = null;

	private bool isAvailable = true;
	public bool IsAvailable { get { return isAvailable; } private set { } }

	public void Interaction(Hand hand)
	{

	}

	public virtual void StartInteraction(Hand hand)
	{
		hand.PickUp();
	}

	public void EndInteraction(Hand hand)
	{
		hand.Drop();
	}

	public void AttachNewSocket(Socket newSocket)
	{
		if (newSocket.StoredObject)
		{
			return;
		}

		ReleaseOldSocket();
		activeSocket = newSocket;

		activeSocket.Attach(this);
		isAvailable = false;
	}

	public void ReleaseOldSocket()
	{
		if (!activeSocket)
		{
			return;
		}

		activeSocket.Detach();
		activeSocket = null;
		isAvailable = true;
	}
}
