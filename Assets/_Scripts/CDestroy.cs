using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class CDestroy : MonoBehaviour
{
	public void destroyObject()
	{
        Destroy(this);
	}
}