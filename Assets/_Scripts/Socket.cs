using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : MonoBehaviour
{
	private Interactable storedObject = null;
	public Interactable StoredObject { get { return storedObject; } private set { } }
	private FixedJoint joint = null;

	private void Awake()
	{
		joint = GetComponent<FixedJoint>();
	}

	public void Attach(Interactable newObject)
	{
		if (!storedObject)
		{
			storedObject = newObject;

			storedObject.transform.position = this.transform.position;
			storedObject.transform.rotation = Quaternion.identity;

			Rigidbody targetBody = storedObject.gameObject.GetComponent<Rigidbody>();
			joint.connectedBody = targetBody;
		}
	}

	public void Detach()
	{
		if (storedObject)
		{
			joint.connectedBody = null;
			storedObject = null;
		}
	}
}
