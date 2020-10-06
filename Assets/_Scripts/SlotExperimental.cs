using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class SlotExperimental : MonoBehaviour
{
	private Valve.VR.InteractionSystem.Interactable storedObject = null;
    public Valve.VR.InteractionSystem.Interactable StoredObject { get { return storedObject; } }
    private FixedJoint joint = null;
	private Vector3 minimazeFactor = new Vector3( 0.5f, 0.5f, 0.5f);

	private void Awake()
	{
		joint = GetComponent<FixedJoint>();
	}

    public void ReleaseObject(Hand hand)
	{
		if (hand)
		{
			if (!hand.currentAttachedObject && storedObject)
			{
				storedObject.transform.localScale = new Vector3(
					storedObject.transform.localScale.x / minimazeFactor.x,
					storedObject.transform.localScale.y / minimazeFactor.y,
					storedObject.transform.localScale.z / minimazeFactor.z);
                storedObject.GetComponent<Collider>().enabled = true;
                hand.AttachObject(storedObject.gameObject, GrabTypes.Grip);
				joint.connectedBody = null;
				storedObject = null;
			}

		}
	}

	public void AttachObject(Hand hand)
	{
		if (hand)
		{
			if (hand.currentAttachedObject)
			{
				storedObject = hand.currentAttachedObject.GetComponent<Valve.VR.InteractionSystem.Interactable>();
				hand.DetachObject(hand.currentAttachedObject);

				storedObject.transform.position = this.transform.position;
				storedObject.transform.rotation = Quaternion.identity;
				storedObject.transform.localScale = Vector3.Scale(storedObject.transform.localScale, minimazeFactor);
                storedObject.GetComponent<Collider>().enabled = false;

				Rigidbody targetBody = storedObject.gameObject.GetComponent<Rigidbody>();
				joint.connectedBody = targetBody;              
			}
		}
	}
}
