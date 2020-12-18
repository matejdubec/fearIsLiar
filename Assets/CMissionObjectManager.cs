using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMissionObjectManager : MonoBehaviour
{
    private CMissionObjectTag activeMissionObjects;

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
                activeMissionObjects = missionObjectTag;
                foreach(Transform ch in child)
                {
                    CGetMissionItem item = ch.GetComponent<CGetMissionItem>();
                    if(item)
                    {
                        item.MissionManager = manager;
                    }
                    else
                    {
                        CCollideMissionObject collideItem = ch.GetComponent<CCollideMissionObject>();
                        if (collideItem)
                        {
                            collideItem.MissionManager = manager;
                        }
                    }

                }
            }
        }
    }

    public void DeactivateActiveMissionOnjects()
    {
        activeMissionObjects.gameObject.SetActive(false);
    }
}
