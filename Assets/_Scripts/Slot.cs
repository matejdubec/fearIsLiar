using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Slot : Interactable
{
	private Interactable storedObject = null;
	private FixedJoint joint = null;
	private Vector3 minimazeFactor = new Vector3( 0.5f, 0.5f, 0.5f);

	private void Awake()
	{
		joint = GetComponent<FixedJoint>();
	}

    public void ReleaseObject(CustomHand hand)
	{
		if (hand)
		{
			if (!hand.CurrentInteractable && storedObject)
			{
				storedObject.transform.localScale = new Vector3(
					storedObject.transform.localScale.x / minimazeFactor.x,
					storedObject.transform.localScale.y / minimazeFactor.y,
					storedObject.transform.localScale.z / minimazeFactor.z);
                storedObject.GetComponent<Collider>().enabled = true;
                hand.PickUp(storedObject);
				joint.connectedBody = null;
				storedObject = null;
			}

		}
	}

	public void AttachObject(CustomHand hand)
	{
		if (hand)
		{
			if (hand.CurrentInteractable)
			{
				storedObject = hand.CurrentInteractable;
				hand.Drop();

				storedObject.transform.position = this.transform.position;
				storedObject.transform.rotation = Quaternion.identity;
				storedObject.transform.localScale = Vector3.Scale(storedObject.transform.localScale, minimazeFactor);
                storedObject.GetComponent<Collider>().enabled = false;

				Rigidbody targetBody = storedObject.gameObject.GetComponent<Rigidbody>();
				joint.connectedBody = targetBody;

                storedObject.DeattachFromHand();
                
			}
		}
	}
}
