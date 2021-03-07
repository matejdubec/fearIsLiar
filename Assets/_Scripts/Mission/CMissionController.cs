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

    private bool missionCompletedSuccessfully = false;


    public void Init()
    {
        missionsDictionary = missionsList.ToDictionary(x => x.MissionId, x => x);
    }

    public void MissionCompleted(bool _missionCompletedSuccessfully, float _completionTime)
    {
        missionCompletedSuccessfully = _missionCompletedSuccessfully;
        activeMission.TimeOfCompletion = _completionTime;
        activeMission = missionsDictionary[EMissionId.NoMission];
    }
}