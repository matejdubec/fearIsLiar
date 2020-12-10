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

    [SerializeField] private SteamVR_LoadLevel loadinator;
    [SerializeField] private CVRController player;
    [SerializeField] private List<CConfigLevel> configLevels;
    private CPlayerData playerData;

    protected override void Init()
    {
        base.Init();
        playerData = CSaveSystem.LoadPlayerData();

        languageManager.Init();
    }

    public void LoadLevel(string levelName)
    {
        loadinator.levelName = levelName;
        loadinator.Trigger();
    }
}
