using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class CBackToMenuCanvasController : MonoBehaviour
{
    [SerializeField] private Button button;
    private float spawnDistance = 5f;

    public void Activate(bool state)
    {
        if (state)
        {
            UpdatePosition();
            CGameManager.Instance.Player.HideFlashlight();
            this.gameObject.SetActive(false);
            CGameManager.Instance.Player.Pointer.gameObject.SetActive(false);
            button.onClick.AddListener(() => { CGameManager.Instance.ReturnToMenu(); });
        }
        else
        {
            button.onClick.RemoveAllListeners();
        }

        this.gameObject.SetActive(state);      
    }

    private void UpdatePosition()
    {
        var head = SteamVR_Render.Top().camera.transform;

        RaycastHit hit;
        Ray ray = new Ray(head.position, head.forward);
        Physics.Raycast(ray, out hit, spawnDistance);

        float closestCollision = hit.distance == 0 ? spawnDistance : hit.distance - 1;

        transform.position = head.position + (head.forward * closestCollision);
        transform.LookAt(head.transform);
        transform.Rotate(0, 180, 0);
    }
}
