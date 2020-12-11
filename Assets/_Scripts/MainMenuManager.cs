using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class MainMenuManager : MonoBehaviour
{
	[SerializeField] private CLevelScrollList scrollList;
	[SerializeField] private AudioMixer audioMixer;

    [SerializeField] private CMissionWaypointsManager missionWaypointsManager;
    private CMissionManager activeMission;
    [SerializeField] private CMissionObjectManager missionObjectManager;

	[SerializeField] private Transform spawnPosition;

    public void Init()
	{
		scrollList.Init();

        activeMission = missionWaypointsManager.GetMissionManager(CGameManager.Instance.MissionController.ActiveMission.MissionId);
        activeMission.Init();
        missionObjectManager.Init(CGameManager.Instance.MissionController.ActiveMission.MissionId, activeMission);
        activeMission.StartMission();
	}

	public void FilterPhobias(string phobiaString)
	{
		EPhobiaId ePhobiaId = (EPhobiaId)Enum.Parse(typeof(EPhobiaId), phobiaString);

		List<CConfigLevel> filteredLevels = (from level in CGameManager.Instance.MissionController.MissionsDictionary.Values where level.PhobiaId == ePhobiaId select level).ToList();

		scrollList.ClearButtons();
		scrollList.AddButtons(filteredLevels);
	}

	public void SetVolume(float volume)
	{
		audioMixer.SetFloat("Volume", volume);
	}

	public void SetQuality(int qualityIndex)
	{
		QualitySettings.SetQualityLevel(qualityIndex);
	}
}
