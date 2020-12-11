using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMissionObjectManager : MonoBehaviour
{
    public void Init(EMissionId activeMissionId, CMissionManager manager)
    {
        foreach (Transform child in this.transform)
        {
            CMissionObjectTag missionObjectTag = child.GetComponent<CMissionObjectTag>();

            if(missionObjectTag.MissionId != activeMissionId)
            {
                child.gameObject.SetActive(false);
            }
            else
            {
                foreach(Transform ch in child)
                {
                    CGetMissionItem item = ch.GetComponent<CGetMissionItem>();
                    if(item)
                    {
                        item.MissionManager = manager;
                    }
                }
            }
        }
    }
}
