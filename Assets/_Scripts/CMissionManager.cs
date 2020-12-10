using System.Collections.Generic;
using UnityEngine;


//https://answers.unity.com/questions/1490192/how-can-i-highlight-the-vive-controller-buttons.html
public class CMissionManager : MonoBehaviour
{
    [SerializeField] private EMissionId missionId;
    public EMissionId MissionId { get { return missionId; } }
    [SerializeField] private CMarker marker;

    [SerializeField] private List<GameObject> objectsToDeactivateOnMissionStart;
    [SerializeField] private List<GameObject> objectsToActivateOnMissionEnd;

    private Queue<CWaypoint> waypointQueue;
    private CWaypoint currentWaypoint = null;

    public void Init()
	{
        DeactivateOnMissionStart();

        waypointQueue = new Queue<CWaypoint>();
        foreach (Transform child in this.transform)
        {
            CWaypoint waypoint = child.GetComponent<CWaypoint>();

            if (waypoint)
            {
                waypoint.Init(this);
                waypointQueue.Enqueue(waypoint);
            }
        }

        marker.Init();
    }

	public void StartMission()
    {
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
        ActivateOnMissionComplete();
    }

    private void DeactivateOnMissionStart()
	{
        foreach (GameObject obj in objectsToDeactivateOnMissionStart)
        {
            obj.SetActive(false);
        }
    }

    private void ActivateOnMissionComplete()
	{
        foreach (GameObject obj in objectsToActivateOnMissionEnd)
        {
            obj.SetActive(true);
        }
    }
}
