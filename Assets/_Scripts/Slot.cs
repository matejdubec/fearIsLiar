using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : Interactable
{
	private Socket socket = null;

	private void Awake()
	{
		socket = GetComponent<Socket>();
	}

	public override void StartInteraction(Hand hand)
	{
		//base.StartInteraction(hand);

		if (hand.HasHeldObject())
		{
			TryStore(hand);
		}
		else
		{
			TryRetrieve(hand);
		}
	}

	private void TryStore(Hand hand)
	{

	}

	private void TryRetrieve(Hand hand)
	{
		if (!socket.StoredObject)
		{
			return;
		}

		Interactable objectToRetrieve = socket.StoredObject;
		hand.PickUp(objectToRetrieve);
	}

    private void OnTriggerEnter(Collider other)
    {
        Hand hand = other.gameObject.GetComponent<Hand>();

        Debug.LogWarning(other.gameObject.name);

        if (socket.StoredObject)
        {
            return;
        }

        if (hand)
        {
            Interactable objectToStore = hand.DropToSlot();

            if(objectToStore)
                objectToStore.AttachNewSocket(socket);
        }
    }
}