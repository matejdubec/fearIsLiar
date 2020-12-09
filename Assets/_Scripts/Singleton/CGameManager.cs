using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

[CSingleton("Singletons/GameManager", true)]
public class CGameManager : CSingleton<CGameManager>
{
    [SerializeField] private CLanguageManager languageManager;
    public CLanguageManager LanguageManager { get { return languageManager; } }
    [SerializeField] private SteamVR_LoadLevel loadinator;
    [SerializeField] private CVRController player;


    protected override void Init()
    {
        base.Init();
        languageManager.Init();
    }

    public void LoadLevel(string levelName)
    {
        loadinator.levelName = levelName;
        loadinator.Trigger();
    }

}
