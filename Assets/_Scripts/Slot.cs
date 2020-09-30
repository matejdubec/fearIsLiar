using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Slot : Interactable
{
	private Interactable storedObject = null;
	private FixedJoint joint = null;

	private void Awake()
	{
		joint = GetComponent<FixedJoint>();
	}

	private void OnCollisionStay(Collision collision)
	{
		Hand hand = collision.gameObject.GetComponent<Hand>();

		if (hand)
		{
			if (hand.CurrentInteractable && hand.GrabAction.GetStateUp(hand.Pose.inputSource) )
			{
				AttachObject(hand);
			}
			else if (!hand.CurrentInteractable && hand.GrabAction.GetStateDown(hand.Pose.inputSource))
			{
				ReleaseObject(hand);
			}
		}
	}

	private void ReleaseObject(Hand hand)
	{
		if (hand)
		{
			if (!hand.CurrentInteractable && storedObject)
			{
				hand.PickUp(storedObject);
				joint.connectedBody = null;
				storedObject = null;
			}

		}
	}

	private void AttachObject(Hand hand)
	{
		if (hand)
		{
			if (hand.CurrentInteractable)
			{
				storedObject = hand.CurrentInteractable;
				hand.Drop();

				storedObject.transform.position = this.transform.position;
				storedObject.transform.rotation = Quaternion.identity;

				Rigidbody targetBody = storedObject.gameObject.GetComponent<Rigidbody>();
				joint.connectedBody = targetBody;
			}
		}
	}
}
