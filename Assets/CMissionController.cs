using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMissionController : MonoBehaviour
{
    [SerializeField] private List<CConfigLevel> missionsList;
    public List<CConfigLevel> MissionsList { get { return missionsList; } }
    private CConfigLevel activeMission;
    public CConfigLevel ActiveMission { get { return activeMission; } set { activeMission = value; } }
}
