using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class CMarker : MonoBehaviour
{
    [SerializeField] private CLocalizationIndentificator hintText;
    public CLocalizationIndentificator HintText { get { return hintText; } }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;

        var head = SteamVR_Render.Top().camera.transform;
        if (head)
        {
            transform.LookAt(head.transform);
            transform.Rotate(Vector3.forward, 0.0);
            transform.Rotate(new Vector3(1,0,0), 0.0);
        }
    }
}
 