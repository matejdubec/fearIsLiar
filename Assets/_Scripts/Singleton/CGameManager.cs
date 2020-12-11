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
    private CPlayerData playerData;

    protected override void Init()
    {
        base.Init();
        
        languageManager.Init();
        missionController.Init();

        var menu = FindObjectOfType<MainMenuManager>();
 

        playerData = CSaveSystem.LoadPlayerData();

        if (!playerData.isTutorialDone)
		{
            missionController.ActiveMission = missionController.MissionsDictionary[EMissionId.Menu_Tutorial];
		}
        else
        {
            missionController.ActiveMission = null;
        }

        menu.Init();
    }

    public void LoadLevel(string levelName)
    {
        loadinator.levelName = levelName;
        loadinator.Trigger();
    }
}
