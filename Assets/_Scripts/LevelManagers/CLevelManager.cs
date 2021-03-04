using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public abstract class CLevelManager : MonoBehaviour
{
    [SerializeField] protected Transform missions;
    [SerializeField] protected Transform spawnPosition;

    protected CMissionTaskManager activeMission;

    //sluzi na debug inych scen
    protected void Awake()
    {
#if UNITY_EDITOR
        CGameManager.CreateInstance();
#endif
    }

    public virtual void Init()
    {
        this.SetMissionTaskManager();
        if(activeMission)
        {
            activeMission.Init(this);
        }
    }

    public virtual void MissionCompleted(bool _missionCompletedSuccessfully)
    {
        CGameManager.Instance.MissionController.MissionCompleted(_missionCompletedSuccessfully);
        CGameManager.Instance.ReturnToMenu();
    }

    public void SpawnPlayer(CVRController player)
    {
        player.SetPosition(spawnPosition.localToWorldMatrix.GetPosition());
    }

    private void SetMissionTaskManager()
    {
        var activeMissionId = CGameManager.Instance.MissionController.ActiveMission.MissionId;

        if(activeMissionId == EMissionId.NoMission)
        {
            missions.gameObject.SetActive(false);
            return;
        }

        foreach (Transform child in missions)
        {
            CMissionTaskManager possibleMissionManager = child.GetComponent<CMissionTaskManager>();
           
            if (possibleMissionManager.MissionId != activeMissionId)
            {
                child.gameObject.SetActive(false);
            }
            else
            {
                activeMission = possibleMissionManager;
            }
        }
    }
}
