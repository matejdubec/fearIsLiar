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
}
