﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pointer : MonoBehaviour
{
	[SerializeField]
	private float m_DefaultLength = 5.0f;
	[SerializeField]
	private GameObject m_Dot = null;

	public Camera Camera { get; private set; } = null;

	private VRInputModule m_InputModule;
	private LineRenderer m_LineRenderer = null;

	private void Awake()
	{
		Camera = GetComponent<Camera>();
		Camera.enabled = false;
		m_LineRenderer = GetComponent<LineRenderer>();
	}

	private void Start()
	{
		m_InputModule = EventSystem.current.gameObject.GetComponent<VRInputModule>();
	}

	// Update is called once per frame
	void Update()
	{
		UpdateLine();
	}

	private void UpdateLine()
	{
		//use default or distance
		PointerEventData data = m_InputModule.Data;
		RaycastHit hit = CreateRaycast();

		//if no hit set default length
		float colliderDistance = hit.distance == 0 ? m_DefaultLength : hit.distance;
		float canvasDistance = data.pointerCurrentRaycast.distance == 0 ? m_DefaultLength : data.pointerCurrentRaycast.distance;

		//get the closet one
		float targetLength = Math.Min(colliderDistance, canvasDistance);

		//default
		Vector3 endPosition = transform.position + (transform.forward * targetLength);

		//set position of the dot
		m_Dot.transform.position = endPosition;

		//set line renderer
		m_LineRenderer.SetPosition(0, transform.position);
		m_LineRenderer.SetPosition(1, endPosition);

	}

	private RaycastHit CreateRaycast()
	{
		RaycastHit hit;
		Ray ray = new Ray(transform.position, transform.forward);
		Physics.Raycast(ray, out hit, m_DefaultLength);

		return hit;
	}
}
