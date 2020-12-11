using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CMissionController : MonoBehaviour
{
    [SerializeField] private List<CConfigLevel> missionsList;
    private Dictionary<EMissionId, CConfigLevel> missionsDictionary;
    public Dictionary<EMissionId, CConfigLevel> MissionsDictionary { get { return missionsDictionary; } }
    private CConfigLevel activeMission;
    public CConfigLevel ActiveMission { get { return activeMission; } set { activeMission = value; } }


    public void Init()
    {
        missionsDictionary = missionsList.ToDictionary(x => x.MissionId, x => x);
    }
}
