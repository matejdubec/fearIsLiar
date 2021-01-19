using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public abstract class CLevelManager : MonoBehaviour
{
    [SerializeField] protected AudioMixer audioMixer;
    [SerializeField] protected VRInputModule inputModule;
    [SerializeField] protected Transform missions;
    [SerializeField] protected Transform spawnPosition;

    protected CMissionTaskManager activeMission;

    public virtual void Init()
    {
        this.SetMissionTaskManager();
        activeMission.Init(this);
        //inputModule.SetPointer(CGameManager.Instance.Player.Pointer);
    }

    public virtual void MissionCompleted()
    {
    }

    public void SpawnPlayer(Transform player)
    {
        player.position = spawnPosition.position;
    }

    private void SetMissionTaskManager()
    {
        var activeMissionId = CGameManager.Instance.MissionController.ActiveMission.MissionId;
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
