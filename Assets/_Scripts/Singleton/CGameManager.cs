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
    [SerializeField] private CVRController player;
    [SerializeField] private List<CConfigLevel> configLevels;
    private CPlayerData playerData;

    protected override void Init()
    {
        base.Init();
        playerData = CSaveSystem.LoadPlayerData();

        if(!playerData.isTutorialDone)
		{
            missionController.ActiveMission.MissionId = EMissionId.Menu_Tutorial;
		}

        languageManager.Init();
    }

    public void LoadLevel(string levelName)
    {
        loadinator.levelName = levelName;
        loadinator.Trigger();
    }
}
