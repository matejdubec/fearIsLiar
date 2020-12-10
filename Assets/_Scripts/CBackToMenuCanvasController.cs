using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class CBackToMenuCanvasController : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private CLocalizationIndentificator buttonText;
    private float spawnDistance = 5f;

    public delegate void ButtonAction();

    public void Activate(bool state)
    {
        if (state)
        {
            UpdatePosition();
        }

        this.gameObject.SetActive(state);
    }

    public void SetButtonText(string identificator)
    {
        buttonText.SetText(identificator);
    }

    public void SetButtonAction(ButtonAction buttonAction)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => { buttonAction(); });
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
