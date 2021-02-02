using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

[CSingleton("Singletons/GameManager", true)]
public class CGameManager : CSingleton<CGameManager>
{

    [SerializeField] private CLanguageManager languageManager;
    public CLanguageManager LanguageManager { get { return languageManager; } }
    [SerializeField] private CMissionController missionController;
    public CMissionController MissionController { get { return missionController; } }
    [SerializeField] private SteamVR_LoadLevel loadinatorPrefab;
    private SteamVR_LoadLevel loadinator;
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
            missionController.ActiveMission = missionController.MissionsDictionary[EMissionId.NoMission];           
        }

        loadinator = Instantiate(loadinatorPrefab, this.transform);

        Instantiate(player.gameObject, this.transform);

        levelManager.Init();
        levelManager.SpawnPlayer(player.transform);        
    }

    public void LoadLevel(CConfigLevel levelToLoad)
    {
       this.missionController.ActiveMission = missionController.MissionsDictionary[levelToLoad.MissionId];

        if(levelToLoad.MissionId == EMissionId.NoMission)
        {
            loadinator.levelName = ESceneId.MainMenu.ToString();
        }
        else
        {
            loadinator.levelName = levelToLoad.SceneId.ToString();
        }

        loadinator.Trigger();

         StartCoroutine("PrepareScene", levelToLoad);
    }

    private IEnumerator PrepareScene(CConfigLevel levelToLoad)
    {
        while(SceneManager.GetActiveScene().name != levelToLoad.SceneId.ToString())
        {
            yield return null;
        }

        levelManager = FindObjectOfType<CLevelManager>();
        levelManager.Init();
        levelManager.SpawnPlayer(player.transform);

        loadinator = Instantiate(loadinatorPrefab, this.transform);
    }

    public void ReturnToMenu()
    {
        //TODO podmienka ci som v main menua lebo nie ak nie som tak return
        LoadLevel(CGameManager.Instance.MissionController.MissionsDictionary[EMissionId.NoMission]);
    }
}
