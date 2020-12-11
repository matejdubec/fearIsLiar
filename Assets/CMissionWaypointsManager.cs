using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMissionWaypointsManager : MonoBehaviour
{
    private CMissionManager missionManager = null;

    public CMissionManager GetMissionManager(EMissionId activeMissionId)
    {
        foreach (Transform child in this.transform)
        {
            CMissionManager possibleMissionManager = child.GetComponent<CMissionManager>();

            if (possibleMissionManager.MissionId != activeMissionId)
            {
                child.gameObject.SetActive(false);
            }
            else
            {
                missionManager = possibleMissionManager;
            }
        }

        return missionManager;
    }
}
