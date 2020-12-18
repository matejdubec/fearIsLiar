using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CLevelManager : MonoBehaviour
{
    [SerializeField] protected AudioMixer audioMixer;

    [SerializeField] protected CMissionWaypointsManager missionWaypointsManager;
    protected CMissionManager activeMission;
    [SerializeField] protected CMissionObjectManager missionObjectManager;

    [SerializeField] protected Transform spawnPosition;

    public virtual void Init()
    {
        activeMission = missionWaypointsManager.GetMissionManager(CGameManager.Instance.MissionController.ActiveMission.MissionId);
        activeMission.Init(this);
        missionObjectManager.Init(CGameManager.Instance.MissionController.ActiveMission.MissionId, activeMission);      
    }

    public virtual void MissionCompleted()
    {
        if (activeMission.deactiveMissionObjectOnComplete)
        {
            missionObjectManager.DeactivateActiveMissionOnjects();
        }
    }

    public void SpawnPlayer(Transform player)
    {
        player.position = spawnPosition.position;
    }
}
