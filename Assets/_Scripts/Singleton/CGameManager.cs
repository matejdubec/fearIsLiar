using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

[CSingleton("Singletons/GameManager", true)]
public class CGameManager : CSingleton<CGameManager>
{

    [SerializeField] private CLanguageManager languageManager;
    public CLanguageManager LanguageManager { get { return languageManager; } }
    [SerializeField] private CMissionController missionController;
    public CMissionController MissionController { get { return missionController; } }
    [SerializeField] private SteamVR_LoadLevel loadinator;
    private CLevelManager levelManager;
    [SerializeField] private CVRController player;
    public CVRController Player { get { return player; } }
    private CPlayerData playerData;

    protected override void Init()
    {
        base.Init();
        
        languageManager.Init();
        missionController.Init();

        levelManager = FindObjectOfType<CLevelManager>();
 

        playerData = CSaveSystem.LoadPlayerData();

        if (!playerData.isTutorialDone)
		{
            missionController.ActiveMission = missionController.MissionsDictionary[EMissionId.Menu_Tutorial];
		}
        else
        {
            missionController.ActiveMission = null;
        }

        Instantiate(player.gameObject, this.transform);

        levelManager.Init();
        levelManager.SpawnPlayer(player.transform);        
    }

    public void LoadLevel(string levelName)
    {
        loadinator.levelName = levelName;
        loadinator.Trigger();
    }
}
