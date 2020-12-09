using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//https://answers.unity.com/questions/1490192/how-can-i-highlight-the-vive-controller-buttons.html
public class CMissionManager : MonoBehaviour
{
    private Queue<CWaypoint> waypointQueue;
    private CWaypoint currentWaypoint = null;
    private bool missionCompleted = false;

    [SerializeField] private CMarker marker;

    [SerializeField] private List<GameObject> objectsToHide;
    public Text InformationText { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject obj in objectsToHide)
        {
            obj.SetActive(false);
        }

        waypointQueue = new Queue<CWaypoint>();
        foreach(Transform child in this.transform)
        {
            CWaypoint waypoint = child.GetComponent<CWaypoint>();

            if (waypoint)
            {
                waypoint.Init(this);
                waypointQueue.Enqueue(waypoint);
            }
        }

        marker.Init();

        NextWaypoint();
    }

    private void NextWaypoint()
    {
        if (waypointQueue.Count > 0)
        {
            currentWaypoint = waypointQueue.Dequeue();
            currentWaypoint.Activate();

            marker.SetPosition(currentWaypoint.transform.position);
            marker.HintText.SetText(currentWaypoint.LocalizationIndentificator);
        }
        else
        {
            MissionCompleted();
        }
    }

    private void MissionCompleted()
    {
        foreach(GameObject obj in objectsToHide)
        {
            obj.SetActive(true);
        }

        //CGameMaster.Instance.BackToMenuCanvasController.SetMainText("Level.MainMenu.Tutorial.Completed");
    }
}
