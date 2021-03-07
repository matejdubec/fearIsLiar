using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CPointer : MonoBehaviour
{
	[SerializeField]
	private float m_DefaultLength = 5.0f;
	[SerializeField]
	private GameObject m_Dot = null;

    [SerializeField] private Camera _camera;
    public Camera Camera { get { return _camera; } }

	private CVRInputModule m_InputModule;
	private LineRenderer m_LineRenderer = null;

    public void Init()
    {
        Camera.enabled = false;
        m_LineRenderer = GetComponent<LineRenderer>();
    }

	private void Start()
	{
        this.Refresh();
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

    public void Refresh()
    {
        m_InputModule = EventSystem.current.gameObject.GetComponent<CVRInputModule>();
        m_InputModule.SetPointer(this);
    }

	public void Activate(bool _activate)
	{
		this.gameObject.SetActive(_activate);
	}
}
