using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class CMarker : MonoBehaviour
{
    [SerializeField] private CLocalizationIndentificator hintText;
    private Transform head = SteamVR_Render.Top().camera.transform;

    private void Update()
	{
        this.SetRotation();
	}

    private void SetRotation()
	{
        if (head)
        {
            transform.LookAt(head.transform);
            transform.Rotate(Vector3.forward, 0.0);
            transform.Rotate(new Vector3(1, 0, 0), 0.0);
        }
    }

    public void SetMarkerOnTask(bool _active, Vector3 _position, string _localizationId)
	{
        if(this.gameObject.activeSelf != _active)
		{
            this.gameObject.SetActive(_active);
        }

        if(this.gameObject.activeSelf)
		{
            transform.position = _position;
            hintText.SetText(_localizationId);
		}
	}
}
 